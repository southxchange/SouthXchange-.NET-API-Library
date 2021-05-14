using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class AddressModel : SxcModel
    {
        /// <summary>
        /// Id of the address
        /// </summary>
        [JsonProperty("Id")]
        public long Id { get; set; }

        /// <summary>
        /// The address
        /// </summary>
        [JsonProperty("Address")]
        public string Address { get; set; }
    }
}
