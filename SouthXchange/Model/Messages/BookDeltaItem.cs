using Newtonsoft.Json;

namespace SouthXchange.Model.Messages
{
    public class BookDeltaItem : BookItem
    {
        [JsonProperty("m")]
        public long MarketId { get; set; }
    }
}
