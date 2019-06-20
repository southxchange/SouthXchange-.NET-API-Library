using SouthXchange;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().RunAsync().Wait();
            Console.ReadKey();
        }

        private async Task RunAsync()
        {
            var context = new SxcContext("Your_API_Key", "Your_API_Secret");

            try
            {
                await TryPrivateAPI(context);
            }
            catch (SxcException e)
            {
                Console.WriteLine(e.Message);
            }

            InitializeWebSockets(context);
        }

        private async Task TryPrivateAPI(SxcContext  context)
        {
            // List balances
            var listBalancesResult = await context.ListBalancesAsync();
            var limxBalance = listBalancesResult.First(r => r.Currency == "LTC");

            if (limxBalance == null)
            {
                throw new Exception("No LTC balance");
            }

            Console.WriteLine(limxBalance);

            // Place order
            var orderCode = await context.PlaceOrderAsync(
                "LTC", "BTC",
                SouthXchange.Model.OrderType.Sell,
                limxBalance.Available,
                (decimal)99999);

            Console.WriteLine(orderCode);

            // List orders
            var listOrdersResult = await context.ListOrdersAsync();
            var order = listOrdersResult.First(r => r.Code == orderCode);

            if (order == null)
            {
                throw new Exception("Order not found");
            }

            Console.WriteLine(order);

            // Cancel order
            await context.CancelOrderAsync(orderCode);

            // Generate new address
            var address = await context.GenerateNewAddressAsync("LTC");

            Console.WriteLine(address);

            // Withdraw
            await context.WithdrawAsync("LTC", address, limxBalance.Available);
        }

        private void InitializeWebSockets(SxcContext context)
        {
            context.RealTimeContext.OnConnected += (s, e) =>
            {
                Console.WriteLine("Connected");
                context.RealTimeContext.Request(7);
                context.RealTimeContext.Subscribe(7);
            };

            context.RealTimeContext.OnDisconnected += (s, e) =>
            {
                Console.WriteLine("Disconnected");
                Thread.Sleep(TimeSpan.FromSeconds(5));
                Console.WriteLine("Reconnecting");
                context.RealTimeContext.Connect();
            };

            context.RealTimeContext.OnMarketBook += (s, e) =>
            {
                e.Data.ToList().ForEach(book => {
                    Console.WriteLine($"book received for market id {book.MarketId}");
                    Console.WriteLine($"bid: {book.Book.OrderByDescending(b => b.Price).FirstOrDefault(b => b.IsBuy)?.Price}");
                    Console.WriteLine($"ask: {book.Book.OrderBy(b => b.Price).FirstOrDefault(b => !b.IsBuy)?.Price}");
                });
            };

            context.RealTimeContext.OnBookDeltaItem += (s, e) =>
            {
                e.Data.ToList().ForEach(b => Console.WriteLine($"price receieved: {b.Price}"));
            };

            context.RealTimeContext.OnTrade += (s, e) =>
            {
                e.Data.ToList().ForEach(b => Console.WriteLine($"trade receieved: {b.Price}"));
            };

            context.RealTimeContext.Connect();
        }
    }
}
