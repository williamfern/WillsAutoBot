using System;

namespace WillsAutoBot.Services.Models
{
    public class CoinPrice : CoinBase
    {
        public decimal Ask { get; set; }
        public decimal Bid { get; set; }
        public decimal Rate { get; set; }
        public string Sell { get; set; }
        public string Buy { get; set; }
        public decimal SpotRate { get; set; }
        public string Market { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string RateType { get; set; }
        public string RateSteps { get; set; }
    }
}