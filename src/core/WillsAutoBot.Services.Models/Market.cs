using System;

namespace WillsAutoBot.Services.Models
{
    public class Market
    {
        public string OrderId { get; set; }
        public string MarketId { get; set; }
        public string BestBid { get; set; }
        public string BestAsk { get; set; }
        public string LastPrice { get; set; }
        public string Volume24h { get; set; }
        public string Price24h { get; set; }
        public string Low24h { get; set; }
        public string High24h { get; set; }
    }
}