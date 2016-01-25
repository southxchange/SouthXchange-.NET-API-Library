using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class PriceResult : SxcModel
    {
        /// <summary>
        /// Market name
        /// </summary>
        [JsonProperty("Market")]
        public string Market { get; set; }

        /// <summary>
        /// Highest buy order
        /// </summary>
        [JsonProperty("Bid")]
        public decimal? Bid { get; set; }

        /// <summary>
        /// Lowest sell order
        /// </summary>
        [JsonProperty("Ask")]
        public decimal? Ask { get; set; }

        /// <summary>
        /// Last price
        /// </summary>
        [JsonProperty("Last")]
        public decimal? Last { get; set; }

        /// <summary>
        /// Last 24 hours last price variation
        /// </summary>
        [JsonProperty("Variation24Hr")]
        public decimal? Variation24Hr { get; set; }

        /// <summary>
        /// Last 24 hours volume
        /// </summary>
        [JsonProperty("Volume24Hr")]
        public decimal Volume24Hr { get; set; }
    }
}
