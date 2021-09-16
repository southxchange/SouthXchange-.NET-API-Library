using Newtonsoft.Json;
using System;

namespace SouthXchange.Model.Messages
{
    public class Order
    {
        [JsonProperty("c")]
        public long Code { get; set; }
        [JsonProperty("m")]
        public long MarketId { get; set; }
        [JsonProperty("d")]
        public DateTime Date { get; set; }
        [JsonProperty("get")]
        public string CurrencyCodeToGet { get; set; }
        [JsonProperty("giv")]
        public string CurrencyCodeToGive { get; set; }
        [JsonProperty("a")]
        public decimal Amount { get; set; }
        [JsonProperty("oa")]
        public decimal OriginalAmount { get; set; }
        [JsonProperty("p")]
        public decimal LimitPrice { get; set; }
        [JsonProperty("b")]
        public bool Buy { get; set; }
    }
}
