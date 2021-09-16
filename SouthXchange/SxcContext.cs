using Newtonsoft.Json;
using SouthXchange.Model;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SouthXchange
{
    public class SxcContext
    {
        #region Attributes

        private string defaultUri = "https://www.southxchange.com/api/v4/";
        private Uri baseUri;
        private string key;
        private string secret;

        #endregion

        #region Constructors

        public SxcContext()
        {
            baseUri = new Uri(defaultUri);
            RealTimeContext = new RealTimeContext(baseUri);
        }

        public SxcContext(string key, string secret)
        {
            this.key = key;
            this.secret = secret;
            baseUri = new Uri(defaultUri);
            RealTimeContext = new RealTimeContext(baseUri);
        }

        public SxcContext(string key, string secret, string baseUrl)
        {
            this.key = key;
            this.secret = secret;
            baseUri = new Uri(baseUrl);
            RealTimeContext = new RealTimeContext(baseUri);
        }

        #endregion

        #region Properties

        public RealTimeContext RealTimeContext { get; private set; }

        #endregion

        #region Public Methods

        #region Public API

        /// <summary>
        /// Lists all markets
        /// </summary>
        /// <returns>
        /// Array of <typeparamref name="MarketResult"/>.
        /// </returns>
        public async Task<MarketResult[]> GetMarketsAsync()
        {
            return (await GetAsync<string[][]>("markets"))
                .Select(m => new MarketResult()
                {
                    ListingCurrency = m[0],
                    ReferenceCurrency = m[1],
                    MarketId = int.Parse(m[2])
                })
                .ToArray();
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

        /// <summary>
        /// Lists history of a specific market between two dates
        /// </summary>
        /// <returns>
        /// Array of <typeparamref name="HistoryResult"/>
        /// </returns>
        public async Task<HistoryResult[]> GetHistory(HistoryRequest historyRequest)
        {
            return await GetAsync<HistoryResult[]>(
                string.Format("history/{0}/{1}/{2}/{3}/{4}",
                historyRequest.ListingCurrency,
                historyRequest.ReferenceCurrency,
                historyRequest.StartJs,
                historyRequest.EndJs,
                historyRequest.Periods?.ToString() ?? string.Empty));
        }

        /// <summary>
        /// Gives information about feed, listed by currency, market and trader level
        /// </summary>
        /// <returns>
        /// <typeparamref name="FeesResult"/>
        /// </returns>
        public async Task<FeesResult> GetFees()
        {
            return await GetAsync<FeesResult>("fees");
        }

        /// <summary>
        /// Gives the status of all wallets
        /// </summary>
        /// <returns>
        /// <typeparamref name="WalletInfo"/>
        /// </returns>
        public async Task<WalletInfo[]> GetWallets()
        {
            return await GetAsync<WalletInfo[]>("wallets");
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
                AmountInReferenceCurrency = false,
                LimitPrice = limitPrice
            });
        }

        /// <summary>
        /// Cancels the given orders. Permission required: Cancel Order
        /// </summary>
        /// <param name="orderCodes">Order codes to cancel</param>
        public async Task CancelOrderAsync(params string[] orderCodes)
        {
            await PostAsync<string>(
                "cancelOrders",
                new CancelOrdersRequest()
                {
                    OrderCodes = orderCodes
                });
        }

        public async Task<string> GetLNInvoiceAsync(string currency, decimal amount)
        {
            return await PostAsync<string>(
                "GetLNInvoice",
                new GetLNInvoiceRequest()
                {
                    Currency = currency,
                    Amount = amount
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
        /// Get order by order code. Permission required: List Orders
        /// </summary>
        /// <returns><typeparamref name="OrderResult"/></returns>
        public async Task<OrderResult> GetOrderAsync(string code)
        {
            return await PostAsync<OrderResult>(
                "getOrder",
                new OrderRequest()
                {
                    Code = code
                });
        }

        /// <summary>
        /// Generates a new address for a given cryptocurrency. Permission required: Generate New Address
        /// </summary>
        /// <param name="currency">Currency for which a new address will be generated</param>
        /// <returns>Generated address</returns>
        public async Task<AddressModel> GenerateNewAddressAsync(string currency)
        {
            return await PostAsync<AddressModel>(
                "generateNewAddress",
                new GenerateNewAddressRequest()
                {
                    Currency = currency
                });
        }

        /// <summary>
        /// Lists addresses for a specific currency. Permission required: Generate New Address
        /// </summary>
        /// <param name="currency">Currency for which the list of addresses will be returned</param>
        /// <param name="pageIndex">The page index</param>
        /// <param name="pageSize">The page size</param>
        /// <returns><typeparamref name="PagedResult"/> of addresses</returns>
        public async Task<PagedResult<AddressModel>> ListAddresses(string currency, int pageIndex, int pageSize)
        {
            return await ListAddresses(new ListAddressesRequest()
            {
                Currency = currency,
                PageIndex = pageIndex,
                PageSize = pageSize
            });
        }

        /// <summary>
        /// Lists addresses for a specific currency. Permission required: Generate New Address
        /// </summary>
        /// <returns><typeparamref name="PagedResult"/> of addresses</returns>
        public async Task<PagedResult<AddressModel>> ListAddresses(ListAddressesRequest listAddressesRequest)
        {
            return await PostAsync<PagedResult<AddressModel>>("listaddresses", listAddressesRequest);
        }

        /// <summary>
        /// Withdraws to a given address. Permission required: Withdraw
        /// </summary>
        /// <param name="withdrawRequest">Instance of <typeparamref name="WithdrawRequest"/></param>
        /// <returns><typeparamref name="WithdrawResult"/></returns>
        public async Task<WithdrawResult> WithdrawAsync(WithdrawRequest withdrawRequest)
        {
            return await PostAsync<WithdrawResult>("withdraw", withdrawRequest);
        }

        /// <summary>
        /// Withdraws to a given address. Permission required: Withdraw
        /// </summary>
        /// <param name="currency">Currency code to withdraw</param>
        /// <param name="address">Destination address</param>
        /// <param name="amount">Amount to withdraw. Destination address will receive this amount minus fees</param>
        /// <returns><typeparamref name="WithdrawResult"/></returns>
        public async Task<WithdrawResult> WithdrawAsync(string currency, string address, decimal amount)
        {
            return await WithdrawAsync(new WithdrawRequest()
            {
                Currency = currency,
                Destination = address,
                DestinationType = IsEmail(address) 
                    ? DestinationType.UserEmailAddress
                    : DestinationType.CryptoAddress,
                Amount = amount
            });
        }

        /// <summary>
        /// Withdraws to a given address. Permission required: Withdraw
        /// </summary>
        /// <param name="currency">Currency code to withdraw</param>
        /// <param name="destination">The withdraw destination address</param>
        /// <param name="destinationType">The withdraw destination type.
        /// 0: Crypto address
        /// 1: Lightning Network invoice
        /// 2: SouthXchange user email address
        /// </param>
        /// <param name="amount">Amount to withdraw. Destination address will receive this amount minus fees</param>
        /// <returns><typeparamref name="WithdrawResult"/></returns>
        public async Task<WithdrawResult> WithdrawAsync(string currency, string destination, DestinationType destinationType, decimal amount)
        {
            return await WithdrawAsync(new WithdrawRequest()
            {
                Currency = currency,
                Destination = destination,
                DestinationType = destinationType,
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

        /// <summary>
        /// Lists transactions for specific currency. Permission required: List Balances
        /// </summary>
        /// <returns><typeparamref name="PagedResult"/> of <typeparamref name="ListTransactionsResult"/></returns>
        public async Task<PagedResult<ListTransactionsResult>> ListTransactionsAsync(string currency, int pageIndex, int pageSize, string sortField = "Date", bool descending = true)
        {
            return await PostAsync<PagedResult<ListTransactionsResult>>("listTransactions", new ListTransactionsRequest()
            {
                Currency = currency,
                PageIndex = pageIndex,
                PageSize = pageSize,
                SortField = sortField,
                Descending = descending
            });
        }

        /// <summary>
        /// Lists transactions for all currencies. Permission required: List Balances
        /// </summary>
        /// <returns>Array of <typeparamref name="ListTransactionsResult"/></returns>
        public async Task<PagedResult<ListTransactionsResult>> ListTransactionsAsync(int pageIndex, int pageSize, string sortField = "Date", bool descending = true)
        {
            return await ListTransactionsAsync(string.Empty, pageIndex, pageSize, sortField, descending);
        }

        /// <summary>
        /// Lists transactions for specific currency. Permission required: List Balances
        /// </summary>
        /// <returns>Array of <typeparamref name="ListTransactionsResult"/></returns>
        public async Task<PagedResult<ListTransactionsResult>> ListTransactionsAsync(ListTransactionsRequest listTransactionsRequest)
        {
            return await PostAsync<PagedResult<ListTransactionsResult>>("listTransactions", listTransactionsRequest);
        }

        /// <summary>
        /// Gets user information. Permission required: Get info
        /// </summary>
        /// <returns>
        /// Instance of <typeparamref name="UserInfo"/>
        /// </returns>
        public async Task<UserInfo> GetUserInfoAsync()
        {
            return await PostAsync<UserInfo>("getUserInfo");
        }

        /// <summary>
        /// Gets the token for private web socket data. Permission required: List Orders and List Balances
        /// </summary>
        /// <returns>
        /// The token
        /// </returns>
        public async Task<string> GetWebSocketToken()
        {
            return await PostAsync<string>("getWebSocketToken");
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
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(secret))
            {
                throw new SxcException("API Key or Secret have not been provided");
            }

            request = request ?? new Request();
            request.Key = key;
            request.Nonce = DateTime.UtcNow.Ticks;

            var jsonData = await Task.Factory.StartNew(
                () => JsonConvert.SerializeObject(request));

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Hash", GetHash(jsonData, secret));

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

        private static bool IsEmail(string address)
        {
            try
            {
                new MailAddress(address);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}