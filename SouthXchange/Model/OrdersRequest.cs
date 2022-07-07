using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class OrdersRequest : PagedRequest
    {
        /// <summary>
        /// Orders codes, i.e: 'CODE': ['1', '2', '3']
        /// </summary>
        [JsonProperty("Code")]
        public string[] Code { get; set; }
    }
}