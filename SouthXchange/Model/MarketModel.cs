using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class MarketModel : SxcModel
    {
        /// <summary>
        /// Listing currency code
        /// </summary>
        [JsonProperty("listingCurrencyCode")]
        public string ListingCurrencyCode { get; set; }

        /// <summary>
        /// Reference currency code
        /// </summary>
        [JsonProperty("referenceCurrencyCode")]
        public string ReferenceCurrencyCode { get; set; }

        /// <summary>
        /// Trading fee for marker orders
        /// </summary>
        [JsonProperty("makerFee")]
        public decimal MakerFee { get; set; }

        /// <summary>
        /// Trading fee for taker orders
        /// </summary>
        [JsonProperty("takerFee")]
        public decimal TakerFee { get; set; }

        /// <summary>
        /// Minimum order amount (in listing currency)
        /// </summary>
        [JsonProperty("minOrderListingCurrency")]
        public decimal? MinOrderListingCurrency { get; set; }

        /// <summary>
        /// The price precision
        /// </summary>
        [JsonProperty("pricePrecision")]
        public int PricePrecision { get; set; }
    }
}
