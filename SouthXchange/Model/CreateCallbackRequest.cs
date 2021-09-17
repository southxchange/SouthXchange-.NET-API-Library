using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class CreateCallbackRequest : Request
    {
        /// <summary>
        /// The callback URL
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// The transactions type triggers
        /// </summary>
        [JsonProperty("transactions")]
        public string[] Transactions { get; set; }
    }
}
