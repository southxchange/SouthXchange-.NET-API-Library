using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class PagedResult<T>
    {
        /// <summary>
        /// Query total results
        /// </summary>
        [JsonProperty("TotalElements")]
        public int TotalResults { get; set; }

        /// <summary>
        /// Results
        /// </summary>
        [JsonProperty("Result")]
        public T[] Result { get; set; }
    }
}
