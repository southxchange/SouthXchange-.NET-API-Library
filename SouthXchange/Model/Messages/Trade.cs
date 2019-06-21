using Newtonsoft.Json;
using System;

namespace SouthXchange.Model.Messages
{
    public class Trade
    {
        [JsonProperty("m")]
        public long MarketId { get; set; }
        [JsonProperty("d")]
        public DateTime Date { get; set; }
        [JsonProperty("b")]
        public bool Buy { get; set; }
        [JsonProperty("a")]
        public decimal Amount { get; set; }
        [JsonProperty("p")]
        public decimal Price { get; set; }
    }
}
