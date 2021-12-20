using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WillsAutoBot.Cointree.Services.HttpClient;
using WillsAutoBot.Services.Models;
using WillsAutoBot.Utilities.Extensions;

namespace WillsAutoBot.Crypto.Services
{
    public class CoinService : ICoinService
    {
        private readonly ICointreeHttpClient _cointreeHttpClient;
        private readonly ILogger<ICoinService> _logger;

        public CoinService(ICointreeHttpClient cointreeHttpClient, ILogger<ICoinService> logger)
        {
            _cointreeHttpClient = cointreeHttpClient.ThrowIfNullOrDefault(nameof(cointreeHttpClient));
            _logger = logger.ThrowIfNullOrDefault(nameof(logger));
        }

        public async Task<List<Coin>> GetCoinList()
        {
            var coinList = new List<Coin>()
            {
                new Coin() { Id = 1, CoinName = "BTC", IsDefault = true },
                new Coin() { Id = 2, CoinName = "ETH", IsDefault = false },
                new Coin() { Id = 3, CoinName = "XRP", IsDefault = false }
            };

            return coinList;
        }

        public async Task<CoinPrice> GetCoinPriceList(string coinName)
        {
            var coin = await _cointreeHttpClient.GetCoinPrice(coinName);

            return new CoinPrice()
            {
                Ask = coin.Ask,
                Bid = coin.Bid,
                Buy = coin.Buy,
                Market = coin.Market,
                Rate = coin.Rate,
                Sell = coin.Sell,
                Timestamp = coin.Timestamp,
                SpotRate = coin.SpotRate,
                RateType = coin.RateType
            };
        }

        public async Task ProcessCoinPriceList()
        {
            var coinList = await GetCoinList();

            foreach (var coin in coinList)
            {
                var coinPrice = await GetCoinPriceList(coin.CoinName);
            }
        }
    }
}