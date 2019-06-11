using Newtonsoft.Json;
using System;

namespace SouthXchange.Model
{
    public class HistoryRequest
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

        [JsonProperty("startJs")]
        internal long StartJs { get; private set; }

        /// <summary>
        /// Start date
        /// </summary>
        [JsonIgnore]
        public DateTime Start {
            get
            {
                return new DateTime(1970, 1, 1).AddMilliseconds(StartJs);
            }
            set
            {
                StartJs = Convert.ToInt64((value - new DateTime(1970, 1, 1)).TotalMilliseconds);
            }
        }

        [JsonProperty("endJs")]
        internal long EndJs { get; private set; }

        /// <summary>
        /// End date
        /// </summary>
        [JsonIgnore]
        public DateTime End
        {
            get
            {
                return new DateTime(1970, 1, 1).AddMilliseconds(EndJs);
            }
            set
            {
                EndJs = Convert.ToInt64((value - new DateTime(1970, 1, 1)).TotalMilliseconds);
            }
        }

        /// <summary>
        /// Number of bars (optional)
        /// </summary>
        [JsonProperty("periods")]
        public int? Periods { get; set; }
    }
}
