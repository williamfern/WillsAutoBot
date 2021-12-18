namespace WillsAutoBot.Settings
{
    public class BtcMarketSettings
    {
        /// <summary>
        /// Domain of BtcMarket API from config settings
        /// </summary>
        public string BaseUrl => "https://api.btcmarkets.net/";

        public string ApiKey { get; set; }

        public string PrivateKey { get; set; }
    }
}
