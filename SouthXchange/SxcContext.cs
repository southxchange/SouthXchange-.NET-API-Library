using Newtonsoft.Json;
using SouthXchange.Model;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SouthXchange
{
    public class SxcContext
    {
        #region Attributes

        private Uri baseUri = new Uri("https://www.southxchange.com/api/");

        private string Key;

        private string Secret;

        #endregion

        #region Constructors

        public SxcContext()
        {
        }

        public SxcContext(string key, string secret)
        {
            Key = key;
            Secret = secret;
        }

        #endregion

        #region Public Methods

        #region Public API

        /// <summary>
        /// Lists all markets
        /// </summary>
        /// <returns>
        /// Array of array.
        /// Index 0: listing currency code.
        /// Index 1: reference currency code.
        /// </returns>
        public async Task<string[][]> GetMarketsAsync()
        {
            return await GetAsync<string[][]>("markets");
        }

        /// <summary>
        /// Gets price of a given market
        /// </summary>
        /// <param name="listingCurrency">Market listing currency</param>
        /// <param name="referenceCurrency">Market reference currency</param>
        /// <returns>
        /// Instance of <typeparamref name="PriceResult"/>
        /// </returns>
        public async Task<PriceResult> GetPrice(string listingCurrency, string referenceCurrency)
        {
            return await GetAsync<PriceResult>(
                string.Format(
                    "price/{0}/{1}",
                    listingCurrency,
                    referenceCurrency));
        }

        /// <summary>
        /// Lists prices of all markets
        /// </summary>
        /// <returns>
        /// Array of <typeparamref name="PriceResult"/>
        /// </returns>
        public async Task<PriceResult[]> GetPrices()
        {
            return await GetAsync<PriceResult[]>("prices");
        }


        /// <summary>
        /// Lists order book of a given market
        /// </summary>
        /// <param name="listingCurrency">Market listing currency</param>
        /// <param name="referenceCurrency">Market reference currency</param>
        /// <returns>
        /// Instance of <typeparamref name="BookResult"/>
        /// </returns>
        public async Task<BookResult> GetBook(string listingCurrency, string referenceCurrency)
        {
            return await GetAsync<BookResult>(
               string.Format(
                   "book/{0}/{1}",
                   listingCurrency,
                   referenceCurrency));
        }

        /// <summary>
        /// Lists latest trades in a given market
        /// </summary>
        /// <param name="listingCurrency">Market listing currency</param>
        /// <param name="referenceCurrency">Market reference currency</param>
        /// <returns>
        /// Array of <typeparamref name="TradeResult"/>
        /// </returns>
        public async Task<TradeResult[]> GetTrades(string listingCurrency, string referenceCurrency)
        {
            return await GetAsync<TradeResult[]>(
               string.Format(
                   "trades/{0}/{1}",
                   listingCurrency,
                   referenceCurrency));
        }

        #endregion

        #region Private API

        /// <summary>
        /// Places an order in a given market. Permission required: Place Order
        /// </summary>
        /// <param name="placeOrderRequest">Instance of <typeparamref name="PlaceOrderRequest"/></param>
        /// <returns>Order code</returns>
        public async Task<string> PlaceOrderAsync(PlaceOrderRequest placeOrderRequest)
        {
            return await PostAsync<string>("placeOrder", placeOrderRequest);
        }

        /// <summary>
        /// Places an order in a given market. Permission required: Place Order
        /// </summary>
        /// <param name="listingCurrency">Market listing currency</param>
        /// <param name="referenceCurrency">Market reference currency</param>
        /// <param name="type">Order type. Possible values: buy, sell</param>
        /// <param name="amount">Order amount in listing currency</param>
        /// <param name="limitPrice">Optional price in reference currency. If null then order is executed at market price</param>
        /// <returns>Order code</returns>
        public async Task<string> PlaceOrderAsync(
            string listingCurrency,
            string referenceCurrency,
            OrderType type,
            decimal amount,
            decimal? limitPrice = null)
        {
            return await PlaceOrderAsync(new PlaceOrderRequest()
            {
                ListingCurrency = listingCurrency,
                ReferenceCurrency = referenceCurrency,
                Type = type,
                Amount = amount,
                LimitPrice = limitPrice
            });
        }

        /// <summary>
        /// Cancels a given order. Permission required: Cancel Order
        /// </summary>
        /// <param name="orderCode">Order code to cancel</param>
        public async Task CancelOrderAsync(string orderCode)
        {
            await PostAsync<string>(
                "cancelOrder",
                new CancelOrderRequest()
                {
                    OrderCode = orderCode
                });
        }

        /// <summary>
        /// Cancels all orders in a given market. Permission required: Cancel Order
        /// </summary>
        /// <param name="cancelMarketOrdersRequest">Instance of <typeparamref name="CancelMarketOrdersRequest"/></param>
        public async Task CancelMarketOrdersAsync(CancelMarketOrdersRequest cancelMarketOrdersRequest)
        {
            await PostAsync<string>("cancelMarketOrders", cancelMarketOrdersRequest);
        }

        /// <summary>
        /// Cancels all orders in a given market. Permission required: Cancel Order
        /// </summary>
        /// <param name="listingCurrency">Market listing currency</param>
        /// <param name="referenceCurrency">Market reference currency</param>
        public async Task CancelMarketOrdersAsync(string listingCurrency, string referenceCurrency)
        {
            await CancelMarketOrdersAsync(new CancelMarketOrdersRequest()
            {
                ListingCurrency = listingCurrency,
                ReferenceCurrency = referenceCurrency
            });
        }

        /// <summary>
        /// Lists all pending orders. Permission required: List Orders
        /// </summary>
        /// <returns>Array of <typeparamref name="ListOrdersResult"/></returns>
        public async Task<ListOrdersResult[]> ListOrdersAsync()
        {
            return await PostAsync<ListOrdersResult[]>("listOrders");
        }

        /// <summary>
        /// Generates a new address for a given cryptocurrency. Permission required: Generate New Address
        /// </summary>
        /// <param name="currency">Currency for which a new address will be generated</param>
        /// <returns>Generated address</returns>
        public async Task<string> GenerateNewAddressAsync(string currency)
        {
            return await PostAsync<string>(
                "generateNewAddress",
                new GenerateNewAddressRequest()
                {
                    Currency = currency
                });
        }

        /// <summary>
        /// Withdraws to a given address. Permission required: Withdraw
        /// </summary>
        /// <param name="withdrawRequest">Instance of <typeparamref name="WithdrawRequest"/></param>
        public async Task WithdrawAsync(WithdrawRequest withdrawRequest)
        {
            await PostAsync<string>("withdraw", withdrawRequest);
        }

        /// <summary>
        /// Withdraws to a given address. Permission required: Withdraw
        /// </summary>
        /// <param name="currency">Currency code to withdraw</param>
        /// <param name="address">Destination address</param>
        /// <param name="amount">Amount to withdraw. Destination address will receive this amount minus fees</param>
        public async Task WithdrawAsync(string currency, string address, decimal amount)
        {
            await WithdrawAsync(new WithdrawRequest()
            {
                Currency = currency,
                Address = address,
                Amount = amount
            });
        }

        /// <summary>
        /// Lists balances for all currencies. Permission required: List Balances
        /// </summary>
        /// <returns>Array of <typeparamref name="ListBalancesResult"/></returns>
        public async Task<ListBalancesResult[]> ListBalancesAsync() 
        {
            return await PostAsync<ListBalancesResult[]>("listBalances");
        }

        #endregion

        #endregion

        #region Private Methods

        private async Task<T> GetAsync<T>(string relativeUri)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(new Uri(baseUri, relativeUri));
            
            if (!response.IsSuccessStatusCode)
            {
                throw new SxcException(response.ReasonPhrase);
            }

            var content = await response.Content.ReadAsStringAsync();

            return await Task.Factory.StartNew(
                () => JsonConvert.DeserializeObject<T>(content));
        }

        private async Task<T> PostAsync<T>(string relativeUri, Request request = null)
        {
            if (string.IsNullOrEmpty(Key) || string.IsNullOrEmpty(Secret))
            {
                throw new Exception("API Key or Secret have not been provided");
            }

            request = request ?? new Request();
            request.Key = Key;
            request.Nonce = DateTime.UtcNow.Ticks;

            var jsonData = await Task.Factory.StartNew(
                () => JsonConvert.SerializeObject(request));

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Hash", GetHash(jsonData, Secret));

            var response = await client.PostAsync(
                new Uri(baseUri, relativeUri),
                new StringContent(jsonData, Encoding.ASCII, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new SxcException(response.ReasonPhrase);
            }

            var content = await response.Content.ReadAsStringAsync();

            return await Task.Factory.StartNew(
                () => JsonConvert.DeserializeObject<T>(content));
        }

        private static string GetHash(string input, string secret)
        {
            HMACSHA512 hash = new HMACSHA512(Encoding.UTF8.GetBytes(secret));
            byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        #endregion
    }
}