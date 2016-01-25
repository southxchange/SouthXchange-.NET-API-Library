using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class BookResult : SxcModel
    {
        public class BookOrderResult
        {
            /// <summary>
            /// Incremental value for each book entry
            /// </summary>
            [JsonProperty("Index")]
            public string Index { get; set; }

            /// <summary>
            /// Book entry total amount
            /// </summary>
            [JsonProperty("Amount")]
            public string Amount { get; set; }

            /// <summary>
            /// Book entry price
            /// </summary>
            [JsonProperty("Price")]
            public string Price { get; set; }
        }

        /// <summary>
        /// Buy orders
        /// </summary>
        [JsonProperty("BuyOrders")]
        public BookOrderResult[] BuyOrders { get; set; }

        /// <summary>
        /// Sell orders
        /// </summary>
        [JsonProperty("SellOrders")]
        public BookOrderResult[] SellOrders { get; set; }
    }
}
