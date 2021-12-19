using System.Collections.Generic;
using System.Threading.Tasks;
using WillsAutoBot.Services.Models;

namespace WillsAutoBot.Crypto.Services
{
    public interface ICoinService
    {
        public Task<List<Coin>> GetCoinList();

        public Task<CoinPrice> GetCoinPriceList(string coinName);

        public Task ProcessCoinPriceList();
    }
}