using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class PagedRequest : Request
    {
        /// <summary>
        /// Page index
        /// </summary>
        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; }

        /// <summary>
        /// Page size. Maximum: 50
        /// </summary>
        [JsonProperty("pageSize")]
        public int PageSize { get; set; }
    }
}
