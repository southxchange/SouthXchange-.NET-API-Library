using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class CancelMarketOrdersRequest : Request
    {
        /// <summary>
        /// Market listing currency
        /// </summary>
        [JsonProperty("listingCurrency")]
        public string ListingCurrency { get; set; }

        /// <summary>
        /// Market reference currency
        /// </summary>
        [JsonProperty("referenceCurrency")]
        public string ReferenceCurrency { get; set; }
    }
}
