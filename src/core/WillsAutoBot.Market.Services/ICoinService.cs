using System.Collections.Generic;
using System.Threading.Tasks;
using WillsAutoBot.Services.Models;

namespace WillsAutoBot.Crypto.Services
{
    public interface ICoinService
    {
        public Task<bool> AddCoin(string name, bool isDefault);

        public Task<List<Coin>> FindAllCoins();

        public Task<CoinPrice> GetCoinPriceList(string coinName);

        public Task ProcessCoinPriceList();
    }
}