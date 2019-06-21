using Newtonsoft.Json;
using System.Collections.Generic;

namespace SouthXchange.Model.Messages
{
    public class MarketBook
    {
        [JsonProperty("m")]
        public long MarketId { get; set; }

        [JsonProperty("b")]
        public IEnumerable<BookItem> Book { get; set; }
    }
}
