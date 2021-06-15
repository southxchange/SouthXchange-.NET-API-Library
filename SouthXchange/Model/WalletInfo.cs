using Newtonsoft.Json;
using System;

namespace SouthXchange.Model
{
    public class WalletInfo
    {
        /// <summary>
        /// Currrency code
        /// </summary>
        [JsonProperty("Currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Last time checked
        /// </summary>
        [JsonProperty("Date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Wallet status
        /// </summary>
        [JsonProperty("Status")]
        public string Status { get; set; }

        /// <summary>
        /// Payment Method
        /// </summary>
        [JsonProperty("Type")]
        public string Type { get; set; }

        /// <summary>
        /// Last block on the blockchain
        /// </summary>
        [JsonProperty("LastBlock")]
        public long LastBlock { get; set; }

        /// <summary>
        /// Current version
        /// </summary>
        [JsonProperty("Version")]
        public string Version { get; set; }

        /// <summary>
        /// Connections count
        /// </summary>
        [JsonProperty("Connections")]
        public int Connections { get; set; }

        /// <summary>
        /// Required number of confirmations
        /// </summary>
        [JsonProperty("RequiredConfirmations")]
        public int RequiredConfirmations { get; set; }
    }
}
