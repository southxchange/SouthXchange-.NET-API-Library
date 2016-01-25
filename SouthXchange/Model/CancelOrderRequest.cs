using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class CancelOrderRequest : Request
    {
        /// <summary>
        /// Order code to cancel
        /// </summary>
        [JsonProperty("orderCode")]
        public string OrderCode { get; set; }
    }
}
