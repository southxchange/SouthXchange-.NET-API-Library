using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class ListCallbacksResult
    {
        /// <summary>
        /// The callback id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        
        /// <summary>
        /// The callback URL
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
        
        /// <summary>
        /// Transactions triggers
        /// </summary>
        [JsonProperty("transactions")]
        public string[] Transactions { get; set; }
    }
}
