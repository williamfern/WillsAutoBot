namespace WillsAutoBot.BTCMarket.Services.BTCMarketDayTrading.Models
{
    public class TradingFee
    {
        public double? tradingFeeRate { get; set; }
        public bool success { get; set; }
        public int? errorCode { get; set; }
        public string errorMessage { get; set; }
        public double? volume30Day { get; set; }
        public double? makerTradingFeeRate { get; set; }
        public double? takerTradingFreeRate { get; set; }
    }
}
