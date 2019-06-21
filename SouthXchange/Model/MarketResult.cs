namespace SouthXchange.Model
{
    public class MarketResult
    {
        /// <summary>
        /// The market ID
        /// </summary>
        public long MarketId { get; set; }
        
        /// <summary>
        /// The market  listing currency
        /// </summary>
        public string ListingCurrency { get; set; }

        /// <summary>
        /// The market base currency
        /// </summary>
        public string ReferenceCurrency { get; set; }
    }
}
