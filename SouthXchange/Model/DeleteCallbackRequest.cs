using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class DeleteCallbackRequest : Request
    {
        /// <summary>
        /// The callback ID
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }
    }
}
