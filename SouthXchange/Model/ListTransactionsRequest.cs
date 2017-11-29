using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class ListTransactionsRequest : SortedPagedRequest
    {
        /// <summary>
        /// Currency code to query
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}
