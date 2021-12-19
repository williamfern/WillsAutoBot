using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WillsAutoBot.Settings;
using WillsAutoBot.Utilities.Extensions;
using WillsAutoBot.Cointree.Services.Models;
using WillsAutoBot.Services.Models;

namespace WillsAutoBot.Cointree.Services.HttpClient
{
    public class CointreeHttpClient : ICointreeHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly CointreeSettings _cointreeSettings;
        private readonly ILogger<CointreeHttpClient> _logger;

        public CointreeHttpClient(IHttpClientFactory httpClientFactory, IOptions<CointreeSettings> cointreeSettings,
            ILogger<CointreeHttpClient> logger)
        {
            _httpClientFactory = httpClientFactory.ThrowIfNullOrDefault(nameof(httpClientFactory));
            _cointreeSettings = cointreeSettings?.Value.ThrowIfNullOrDefault(nameof(cointreeSettings));
            _logger = logger.ThrowIfNullOrDefault(nameof(logger));
        }

        public async Task<CoinPriceResponse> GetCoinPrice(string coinName)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_cointreeSettings.BaseUrl);

            var response = await client.GetAsync(_cointreeSettings.BaseUrl + "api/prices/aud/" + coinName);
            if (!response.IsSuccessStatusCode)
                _logger.LogError("Error: " + response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CoinPriceResponse>(content);
        }
    }
}