using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class UserInfo : SxcModel
    {
        /// <summary>
        /// User trader level name
        /// </summary>
        [JsonProperty("TraderLevel")]
        public string TraderLevel { get; set; }
    }
}
