namespace WillsAutoBot.BTCMarket.Services.BTCMarketDayTrading.Models
{
    public class Transaction
    {
        public long id { get; set; }
        public long balance { get; set; }
        public long amount { get; set; }
        public string action { get; set; }
        public string recordType { get; set; }
        public string currency { get; set; }
    }
}