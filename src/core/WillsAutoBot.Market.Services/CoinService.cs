using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WillsAutoBot.Cointree.Services.HttpClient;
using WillsAutoBot.Data.Entities;
using WillsAutoBot.Data.Repositories;
using WillsAutoBot.Services.Models;
using WillsAutoBot.Utilities.Extensions;

namespace WillsAutoBot.Crypto.Services
{
    public class CoinService : ICoinService
    {
        private readonly ICointreeHttpClient _cointreeHttpClient;
        private readonly ICoinRepository _coinRepository;
        private readonly ICoinPriceRepository _coinPriceRepository;

        private readonly IMapper _mapper;
        private readonly ILogger<ICoinService> _logger;

        public CoinService(ICointreeHttpClient cointreeHttpClient, 
            ICoinRepository coinRepository, 
            ICoinPriceRepository coinPriceRepository, 
            ILogger<ICoinService> logger,
            IMapper mapper)
        {
            _cointreeHttpClient = cointreeHttpClient.ThrowIfNullOrDefault(nameof(cointreeHttpClient));
            _coinRepository = coinRepository.ThrowIfNullOrDefault(nameof(coinRepository));
            _coinPriceRepository = coinPriceRepository.ThrowIfNullOrDefault(nameof(coinPriceRepository));
            _logger = logger.ThrowIfNullOrDefault(nameof(logger));
            _mapper = mapper.ThrowIfNullOrDefault(nameof(mapper));
        }

        public async Task<List<Coin>> FindAllCoins()
        {
            var coinList = await _coinRepository.FindAll();

            return _mapper.Map< List<Coin>>(coinList);
        }

        public async Task<bool> AddCoin(string name, bool isDefault)
        {
            await _coinRepository.Add(new CoinEntity
            {
                CoinId = Guid.NewGuid().ToString(),
                CoinName = name,
                IsDefault = isDefault
            });

            return true;
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
            var coinList = await FindAllCoins();

            if (coinList.Count == 0)
            {
                _logger.LogInformation("Coin list is empty");
                //    // adding coins
                //    var coinListToAdd = new List<Coin>() {
                //            new Coin() { Id = 1, CoinName = "BTC", IsDefault = true },
                //            new Coin() { Id = 2, CoinName = "ETH", IsDefault = false },
                //            new Coin() { Id = 3, CoinName = "XRP", IsDefault = false }
                //        };

                //    foreach (var coin in coinListToAdd)
                //    {
                //       await AddCoin(coin.CoinName, coin.IsDefault);
                //    }

                //    _logger.LogInformation("Coin list Added");
                //    coinList = await FindAllCoins();
            
            }
            
            foreach (var coin in coinList)
            {
                var coinPrice = await GetCoinPriceList(coin.CoinName);

                var coinPriceEntity = _mapper.Map<CoinPriceEntity>(coinPrice);

                coinPriceEntity.CoinId = coin.CoinId;
                coinPriceEntity.CoinPriceId = Guid.NewGuid().ToString();
                coinPriceEntity.CoinName = coin.CoinName;

                // Save the coin price details to storage 
                await _coinPriceRepository.AddOrUpdate(coinPriceEntity);
            }
        }
    }
}