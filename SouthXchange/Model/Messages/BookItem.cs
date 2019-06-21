using Newtonsoft.Json;

namespace SouthXchange.Model.Messages
{
    public class BookItem
    {
        [JsonProperty("p")]
        public decimal Price { get; set; }
        [JsonProperty("a")]
        public decimal Amount { get; set; }
        [JsonProperty("b")]
        public bool IsBuy { get; set; }
    }
}
