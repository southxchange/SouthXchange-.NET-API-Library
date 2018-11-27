using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class OrderResult : SxcModel
    {
        /// <summary>
        /// Order code
        /// </summary>
        [JsonProperty("Code")]
        public string Code { get; set; }

        /// <summary>
        /// Order type. Possible values: buy, sell
        /// </summary>
        [JsonProperty("Type")]
        internal string TypeString { get; set; }

        /// <summary>
        /// Order type. Possible values: buy, sell
        /// </summary>
        [JsonIgnore]
        public OrderType Type
        {
            get 
            {
                return TypeString == "buy"
                    ? OrderType.Buy
                    : OrderType.Sell;
            }
            set
            {
                TypeString = value == OrderType.Buy
                    ? "buy"
                    : "sell";
            }
        }

        /// <summary>
        /// Amount in listing currency
        /// </summary>
        [JsonProperty("Amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Order price in reference currency
        /// </summary>
        [JsonProperty("LimitPrice")]
        public decimal LimitPrice { get; set; }

        /// <summary>
        /// Market listing currency
        /// </summary>
        [JsonProperty("ListingCurrency")]
        public string ListingCurrency { get; set; }

        /// <summary>
        /// Market reference currency
        /// </summary>
        [JsonProperty("ReferenceCurrency")]
        public string ReferenceCurrency { get; set; }

        /// <summary>
        /// Status of the order
        /// </summary>
        [JsonProperty("Status")]
        public string Status { get; set; }

        /// <summary>
        /// Order creation date
        /// </summary>
        [JsonProperty("DateAdded")]
        public string DateAdded { get; set; }
    }
}
