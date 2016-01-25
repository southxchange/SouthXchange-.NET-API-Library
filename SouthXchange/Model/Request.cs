using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class Request : SxcModel
    {
        /// <summary>
        /// API Key
        /// </summary>
        [JsonProperty("key")]
        internal string Key { get; set; }

        /// <summary>
        /// Positive numerical value greater than the one used in the previous call
        /// </summary>
        [JsonProperty("nonce")]
        internal long Nonce { get; set; }
    }
}