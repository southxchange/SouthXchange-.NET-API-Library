using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class PagedRequest : Request
    {
        /// <summary>
        /// Page index (0-based)
        /// </summary>
        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        [JsonProperty("pageSize")]
        public int PageSize { get; set; }
    }
}
