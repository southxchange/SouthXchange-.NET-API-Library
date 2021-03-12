using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class FeesResult : SxcModel
    {
        /// <summary>
        /// Array of currency information
        /// </summary>
        [JsonProperty("currencies")]
        public CurrencyModel[] Currencies { get; set; }

        /// <summary>
        /// Array of market information
        /// </summary>
        [JsonProperty("markets")]
        public MarketModel[] Markets { get; set; }

        /// <summary>
        /// Array of trader levels
        /// </summary>
        [JsonProperty("traderLevels")]
        public TraderLevelModel[] TraderLevels { get; set; }
    }
}
