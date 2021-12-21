using System;

namespace WillsAutoBot.WebApi.Models
{
    public class CoinPriceApiModel : PriceBaseApiModel
    {
        public int CoinId { get; set; }
        public string Sell { get; set; }
        public string Buy { get; set; }
        public decimal SpotRate { get; set; }
        public string Market { get; set; }
        public DateTime Timestamp { get; set; }
        public string RateType { get; set; }
        public string RateSteps { get; set; }
    }
}