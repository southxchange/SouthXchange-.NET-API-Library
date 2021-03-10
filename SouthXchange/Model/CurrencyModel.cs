using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class CurrencyModel : SxcModel
    {
        /// <summary>
        /// Currency code
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// Currency name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Number of decimals
        /// </summary>
        [JsonProperty("precision")]
        public int Precision { get; set; }

        /// <summary>
        /// Minimum deposit amount
        /// </summary>
        [JsonProperty("minDeposit")]
        public decimal MinDeposit { get; set; }

        /// <summary>
        /// Minimum deposit fee
        /// </summary>
        [JsonProperty("depositFeeMin")]
        public decimal DepositFeeMin { get; set; }

        /// <summary>
        /// Minimum withdraw amount
        /// </summary>
        [JsonProperty("minWithdraw")]
        public decimal MinWithdraw { get; set; }

        /// <summary>
        /// Withdraw fee
        /// </summary>
        [JsonProperty("withdrawFee")]
        public decimal WithdrawFee { get; set; }

        /// <summary>
        /// Minimum withdraw fee
        /// </summary>
        [JsonProperty("withdrawFeeMin")]
        public decimal WithdrawFeeMin { get; set; }

    }
}
