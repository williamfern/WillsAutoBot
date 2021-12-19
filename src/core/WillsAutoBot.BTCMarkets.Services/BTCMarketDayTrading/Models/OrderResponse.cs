using System.Collections.Generic;

namespace WillsAutoBot.BTCMarket.Services.BTCMarketDayTrading.Models
{
    public class OrderResponse
    {
        public bool success { get; set; }
        public int? errorCode { get; set; }
        public string errorMessage { get; set; }
        public List<Order> orders { get; set; }
    }
}