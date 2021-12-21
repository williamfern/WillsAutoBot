using System;

namespace WillsAutoBot.WebApi.Models
{
    public class CoinApiModel
    {
        public int CoinId { get; set; }
        public string CoinName { get; set; }
        public bool IsDefault { get; set; }
    }
}