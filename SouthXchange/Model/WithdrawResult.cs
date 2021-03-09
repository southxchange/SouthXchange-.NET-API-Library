using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class WithdrawResult
    {
        /// <summary>
        /// Status of the withdraw
        /// 
        /// Possible values:
        /// ok: withdrawal request succees
        /// holdsNotSatisfied: cannot withdraw due to hold in your account
        /// limitReached: cannot withdraw due to withdrawal limit
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// The remaining withdrawal limit
        /// </summary>
        [JsonProperty("max")]
        public decimal? Max { get; set; }

        /// <summary>
        /// Maximum daily withdraw limit
        /// </summary>
        [JsonProperty("maxDaily")]
        public decimal? MaxDaily { get; set; }

        /// <summary>
        /// ID of this withdrawal
        /// </summary>
        [JsonProperty("movementId")]
        public decimal? MovementId { get; set; }
    }
}
