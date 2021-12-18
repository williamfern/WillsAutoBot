using System.Collections.Generic;

namespace WillsAutoBot.Services.Models
{
    public class PriceCheck : CoinBase
    {
        public CoinBase PriceChange { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
    }
}