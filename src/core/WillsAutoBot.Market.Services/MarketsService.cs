using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WillsAutoBot.BTCMarket.Services.Constants;
using WillsAutoBot.BTCMarket.Services.HttpClient;
using WillsAutoBot.Services.Models;
using WillsAutoBot.Utilities.Extensions;

namespace WillsAutoBot.Crypto.Services
{
    public class MarketsService : IMarketsService
    {
        private readonly IBtcMarketHttpClient _btcMarketHttpClient;
        private readonly ILogger<IMarketsService> _logger;
        
        public MarketsService(
            IBtcMarketHttpClient btcMarketHttpClient, 
            ILogger<IMarketsService> logger)
        {
            _btcMarketHttpClient = btcMarketHttpClient.ThrowIfNullOrDefault(nameof(btcMarketHttpClient));
            _logger = logger.ThrowIfNullOrDefault(nameof(logger));
        }


        public async Task GetActiveMarket()
        {
            var response = await _btcMarketHttpClient.Get(VersionConstants.Version3 + MethodConstants.MARKET_TRADES_PATH, "status=Online");

            var res = JsonSerializer.Deserialize<List<Market>>(response.Content);
        }
    }
}