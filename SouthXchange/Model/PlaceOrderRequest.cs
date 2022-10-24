﻿using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class PlaceOrderRequest : Request
    {
        /// <summary>
        /// Market listing currency
        /// </summary>
        [JsonProperty("listingCurrency")]
        public string ListingCurrency { get; set; }

        /// <summary>
        /// Market reference currency
        /// </summary>
        [JsonProperty("referenceCurrency")]
        public string ReferenceCurrency { get; set; }

        /// <summary>
        /// Order type. Possible values: buy, sell
        /// </summary>
        [JsonIgnore]
        public OrderType Type { get; set; }

        /// <summary>
        /// Order type. Possible values: buy, sell
        /// </summary>
        [JsonProperty("type")]
        internal string TypeString
        {
            get
            {
                return Type == OrderType.Buy
                    ? "buy"
                    : "sell";
            }
        }
        /// <summary>
        /// Order Execution type. Posible values GTC, FOK
        /// </summary>
        [JsonProperty("ordertype")]
        public OrderExcecutionType OrdersExecutionType { get; set; } = OrderExcecutionType.GTC;

        /// <summary>
        /// Order amount
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Specifies whether Amount is in reference currency.
        /// Default value: false. Amount is sent in listing currency.
        /// </summary>
        [JsonProperty("amountInReferenceCurrency")]
        public bool AmountInReferenceCurrency { get; set; }

        /// <summary>
        /// Optional price in reference currency. If null then order is executed at market price
        /// </summary>
        [JsonProperty("limitPrice")]
        public decimal? LimitPrice { get; set; }
    }
}
