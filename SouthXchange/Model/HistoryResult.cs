using Newtonsoft.Json;
using System;

namespace SouthXchange.Model
{
    public class HistoryResult
    {
        /// <summary>
        /// Bar date
        /// </summary>
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Open price
        /// </summary>
        [JsonProperty("priceOpen")]
        public decimal PriceOpen { get; set; }

        /// <summary>
        /// Higher price
        /// </summary>
        [JsonProperty("priceHigh")]
        public decimal PriceHigh { get; set; }

        /// <summary>
        /// Lower price
        /// </summary>
        [JsonProperty("priceLow")]
        public decimal PriceLow { get; set; }

        /// <summary>
        /// Close price
        /// </summary>
        [JsonProperty("priceClose")]
        public decimal PriceClose { get; set; }

        /// <summary>
        /// Volume
        /// </summary>
        [JsonProperty("volume")]
        public decimal Volume { get; set; }
    }
}
