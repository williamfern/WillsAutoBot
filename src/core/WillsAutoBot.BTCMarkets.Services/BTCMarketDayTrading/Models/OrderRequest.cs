namespace WillsAutoBot.BTCMarket.Services.BTCMarketDayTrading.Models
{
    public class OrderRequest
    {
        public string currency { get; set; }
        public string instrument { get; set; }
        public long limit { get; set; }
        public long? since { get; set; }
    }
}