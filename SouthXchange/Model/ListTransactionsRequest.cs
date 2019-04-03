using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class ListTransactionsRequest : SortedPagedRequest
    {
        public ListTransactionsRequest()
        {
            SortField = "Date";
            Descending = true;
        }

        /// <summary>
        /// Currency code to query
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Transaction types: transactions, deposits, withdrawals, depositwithdrawals
        /// </summary>
        [JsonProperty("transactionType")]
        public string TransactionType { get; set; }
    }
}
