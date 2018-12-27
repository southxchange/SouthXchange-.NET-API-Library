using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class WithdrawResult
    {
        /// <summary>
        /// Status of the withdraw
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Maximum withdraw limit left
        /// </summary>
        [JsonProperty("max")]
        public decimal? Max { get; set; }

        /// <summary>
        /// Maximum daily withdraw limit
        /// </summary>
        [JsonProperty("maxDaily")]
        public decimal? MaxDaily { get; set; }
    }
}
