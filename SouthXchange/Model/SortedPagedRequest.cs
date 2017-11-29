using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class SortedPagedRequest : PagedRequest
    {
        /// <summary>
        /// Sort field
        /// </summary>
        [JsonProperty("sortField")]
        public string SortField { get; set; }

        /// <summary>
        /// Sort direction
        /// </summary
        [JsonProperty("descending")]
        public bool Descending { get; set; }
    }
}
