using System;

namespace WillsAutoBot.Services.Models
{
    public class Market
    {
        public string marketId { get; set; }
        public string baseAssetName { get; set; }
        public string quoteAssetName { get; set; }
        public string minOrderAmount { get; set; }
        public string maxOrderAmount { get; set; }
        public string priceDecimals { get; set; }
        public string amountDecimals { get; set; }
        public string status { get; set; }
    }
}