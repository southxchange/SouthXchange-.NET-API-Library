using SouthXchange;
using SouthXchange.Model;
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
            var context = new SxcContext("YOUR_API_KEY", "YOUR_API_SECRET");

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
            var currency = "BTC";
            var marketCurrency = "USD";

            // List balances
            var listBalancesResult = await context.ListBalancesAsync();
            var currencyBalance = listBalancesResult.First(r => r.Currency == currency);
            
            if (currencyBalance == null)
            {
                throw new Exception($"No {currency} balance");
            }
            
            Console.WriteLine(currencyBalance);
            
            // Place order
            var orderCode = await context.PlaceOrderAsync(
                currency, marketCurrency,
                OrderType.Sell,
                (decimal)0.00000001,
                1000000);
            
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
            var address = await context.GenerateNewAddressAsync(currency);
            
            Console.WriteLine(address);
            
            // Withdraw
            // await context.WithdrawAsync(currency, address, DestinationType.CryptoAddress, currencyBalance.Available);

            // get LN Invoice
            var invoice = await context.GetLNInvoiceAsync(currency, (decimal)0.01);
            Console.WriteLine(invoice);
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
