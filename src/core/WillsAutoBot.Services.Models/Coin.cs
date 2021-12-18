using System;

namespace WillsAutoBot.Services.Models
{
    public class Coin
    {
        public int Id { get; set; }
        public string CoinName { get; set; }
        public bool IsDefault { get; set; }
    }
}