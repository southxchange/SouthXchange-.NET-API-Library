using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class TraderLevelModel : SxcModel
    {
        /// <summary>
        /// Trader level name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Minimum volume amount required
        /// </summary>
        [JsonProperty("minVolumeAmount")]
        public decimal MinVolumeAmount { get; set; }

        /// <summary>
        /// Minimum volume amount currency
        /// </summary>
        [JsonProperty("minVolumeCurrency")]
        public string MinVolumeCurrency { get; set; }

        /// <summary>
        /// Maker orders trading fee rebate
        /// </summary>
        [JsonProperty("makerFeeRebate")]
        public decimal MakerFeeRebate { get; set; }

        /// <summary>
        /// Taker orders trading fee rebate
        /// </summary>
        [JsonProperty("takerFeeRebate")]
        public string TakerFeeRebate { get; set; }

    }
}
