using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class ListAddressesRequest : PagedRequest
    {
        /// <summary>
        /// Currency for which a new address will be generated
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}
