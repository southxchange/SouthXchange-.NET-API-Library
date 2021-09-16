using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class CancelOrdersRequest : Request
    {
        /// <summary>
        /// Order codes to cancel
        /// </summary>
        [JsonProperty("orderCodes")]
        public string[] OrderCodes { get; set; }
    }
}
