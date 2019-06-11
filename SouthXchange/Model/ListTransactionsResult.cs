using Newtonsoft.Json;
using System;

namespace SouthXchange.Model
{
    public class ListTransactionsResult
    {
        /// <summary>
        /// Date
        /// </summary>
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Currency code
        /// </summary>
        [JsonProperty("currencyCode")]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// TotalBalance
        /// </summary>
        [JsonProperty("totalBalance")]
        public decimal? TotalBalance { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// Hash
        /// </summary>
        [JsonProperty("hash")]
        public string Hash { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// OtherAmount
        /// </summary>
        [JsonProperty("otherAmount")]
        public decimal OtherAmount { get; set; }

        /// <summary>
        /// OtherCurrency
        /// </summary>
        [JsonProperty("otherCurrency")]
        public string OtherCurrency { get; set; }

        /// <summary>
        /// OrderCode
        /// </summary>
        [JsonProperty("orderCode")]
        public string OrderCode { get; set; }

        /// <summary>
        /// TradeId
        /// </summary>
        [JsonProperty("tradeId")]
        public string TradeId { get; set; }
    }
}
