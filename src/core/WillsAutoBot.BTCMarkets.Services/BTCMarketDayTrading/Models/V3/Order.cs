using System;

namespace WillsAutoBot.BTCMarket.Services.BTCMarketDayTrading.Models.V3
{
    public class Order
    {
        public string OrderId { get; set; }
        public string MarketId { get; set; }
        public string Side { get; set; }
        public string Type { get; set; }
        public DateTime CreationTime { get; set; }
        public string Price { get; set; }
        public string Amount { get; set; }
        public string OpenAmount { get; set; }
        public string Status { get; set; }
    }
}