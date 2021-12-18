using System.Collections.Generic;
using System.Threading.Tasks;
using WillsAutoBot.Services.Models;

namespace WillsAutoBot.Crypto.Services
{
    public class CoinService : ICoinService
    {
        public async Task<List<Coin>> GetCoinList()
        {
            var coinList = new List<Coin>() {
                new Coin() { Id = 1, CoinName = "BTC", IsDefault = true },
                new Coin() { Id = 2, CoinName = "ETH", IsDefault = false },
                new Coin() { Id = 3, CoinName = "XRP", IsDefault = false }
            };

            return coinList;
        }
    }
}