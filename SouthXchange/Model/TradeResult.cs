using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class TradeResult : SxcModel
    {
        /// <summary>
        /// Execution time in Unix standard format (UTC)
        /// </summary>
        [JsonProperty("At")]
        public long At { get; set; }

        /// <summary>
        /// Amount in listing currency
        /// </summary>
        [JsonProperty("Amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Price in reference currency
        /// </summary>
        [JsonProperty("Price")]
        public decimal Price { get; set; }

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
        }
    }
}
