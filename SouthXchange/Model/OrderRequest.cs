using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class OrderRequest : Request
    {
        /// <summary>
        /// Order code
        /// </summary>
        [JsonProperty("Code")]
        public string Code { get; set; }
    }
}
