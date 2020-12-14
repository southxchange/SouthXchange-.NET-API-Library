using Newtonsoft.Json;

namespace SouthXchange.Model
{
    class GetLNInvoiceRequest: Request
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
