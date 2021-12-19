using System.Collections.Generic;

namespace WillsAutoBot.BTCMarket.Services.BTCMarketDayTrading.Models
{
    public class Transactions
    {
        public bool success { get; set; }
        public IEnumerable<Transaction> transactions { get; set; }
    }
}