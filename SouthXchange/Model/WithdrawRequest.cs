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
        /// The withdraw destination address
        /// </summary>
        [JsonProperty("destination")]
        public string Destination { get; set; }

        /// <summary>
        /// 0: Crypto address
        /// 1: Lightning Network invoice
        /// 2: SouthXchange user email address
        /// </summary>
        [JsonProperty("destinationType")]
        public DestinationType DestinationType { get; set; }

        /// <summary>
        /// Amount to withdraw. Destination address will receive this amount minus fees
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
