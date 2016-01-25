using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class ListBalancesResult : SxcModel
    {
        /// <summary>
        /// Currency code
        /// </summary>
        [JsonProperty("Currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Total amount deposited for this currency code
        /// </summary>
        [JsonProperty("Deposited")]
        public decimal Deposited { get; set; }

        /// <summary>
        /// Total amount that is not committed in orders
        /// </summary>
        [JsonProperty("Available")]
        public decimal Available { get; set; }

        /// <summary>
        /// Total amount unconfirmed in pending deposits
        /// </summary>
        [JsonProperty("Unconfirmed")]
        public decimal Unconfirmed { get; set; }
    }
}
