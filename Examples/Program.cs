using SouthXchange;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().RunAsync().Wait();            
        }

        private async Task RunAsync()
        {
            var context = new SxcContext("Your_API_Key", "Your_API_Secret");

            // List balances
            var listBalancesResult = await context.ListBalancesAsync();
            var limxBalance = listBalancesResult.First(r => r.Currency == "LIMX");
            
            if (limxBalance == null)
            {
                throw new Exception("No LIMX balance");
            }

            Console.WriteLine(limxBalance);

            // Place order
            var orderCode = await context.PlaceOrderAsync(
                "LIMX", "BTC",
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
            var address = await context.GenerateNewAddressAsync("LIMX");

            Console.WriteLine(address);

            // Withdraw
            await context.WithdrawAsync("LIMX", address, limxBalance.Available);
        }
    }
}
