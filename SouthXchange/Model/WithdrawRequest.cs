using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class WithdrawRequest : Request
    {
        /// <summary>
        /// Currency code to withdraw
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Destination address
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// Amount to withdraw. Destination address will receive this amount minus fees
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
