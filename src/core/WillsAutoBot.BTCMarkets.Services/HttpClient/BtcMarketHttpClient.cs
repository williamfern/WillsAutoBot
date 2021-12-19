using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WillsAutoBot.Settings;
using WillsAutoBot.Utilities.Extensions;
using WillsAutoBot.BTCMarket.Services.Models;

namespace WillsAutoBot.BTCMarket.Services.HttpClient
{
    public class BtcMarketHttpClient : IBtcMarketHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly BtcMarketSettings _btcMarketSettings;
        private readonly ILogger<BtcMarketHttpClient> _logger;

        public BtcMarketHttpClient(IHttpClientFactory httpClientFactory, IOptions<BtcMarketSettings> btcMarketSettings,
            ILogger<BtcMarketHttpClient> logger)
        {
            _httpClientFactory = httpClientFactory.ThrowIfNullOrDefault(nameof(httpClientFactory));
            _btcMarketSettings = btcMarketSettings?.Value.ThrowIfNullOrDefault(nameof(btcMarketSettings));
            _logger = logger.ThrowIfNullOrDefault(nameof(logger));
        }

        public async Task<ResponseModel> Get(string path, string queryString)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_btcMarketSettings.BaseUrl);
            GenerateHeaders(client, "GET", null, path);

            var fullPath = !string.IsNullOrEmpty(queryString) ? path + "?" + queryString : path;

            var response = await client.GetAsync(fullPath);
            if (!response.IsSuccessStatusCode)
                Console.WriteLine("Error: " + response.StatusCode.ToString());

            var content = await response.Content.ReadAsStringAsync();
            return new ResponseModel
            {
                Headers = response.Headers,
                Content = await response.Content.ReadAsStringAsync()
            };
        }

        public async Task<string> Post(string path, string queryString, object data)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_btcMarketSettings.BaseUrl);
            var stringifiedData = data != null ? JsonConvert.SerializeObject(data) : null;
            GenerateHeaders(client, "POST", stringifiedData, path);

            var fullPath = !string.IsNullOrEmpty(queryString) ? path + "?" + queryString : path;
            var content = new StringContent(stringifiedData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(fullPath, content);
            if (!response.IsSuccessStatusCode)
                Console.WriteLine("Error: " + response.StatusCode.ToString());

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Put(string path, string queryString, object data)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_btcMarketSettings.BaseUrl);
            var stringifiedData = data != null ? JsonConvert.SerializeObject(data) : null;
            GenerateHeaders(client, "PUT", stringifiedData, path);

            var fullPath = !string.IsNullOrEmpty(queryString) ? path + "?" + queryString : path;
            var content = new StringContent(stringifiedData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(fullPath, content);
            if (!response.IsSuccessStatusCode)
                Console.WriteLine("Error: " + response.StatusCode.ToString());

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Delete(string path, string queryString)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_btcMarketSettings.BaseUrl);
            GenerateHeaders(client, "DELETE", null, path);

            var fullPath = !string.IsNullOrEmpty(queryString) ? path + "?" + queryString : path;

            var response = await client.DeleteAsync(fullPath);
            if (!response.IsSuccessStatusCode)
                Console.WriteLine("Error: " + response.StatusCode.ToString());

            return await response.Content.ReadAsStringAsync();
        }


        private void GenerateHeaders(System.Net.Http.HttpClient client, string method, string data, string path)
        {
            long now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            var message = method + path + now.ToString();
            if (!string.IsNullOrEmpty(data))
                message += data;

            string signature = SignMessage(message);
            client.DefaultRequestHeaders.Add("Accept", "application /json");
            client.DefaultRequestHeaders.Add("Accept-Charset", "UTF-8");
            client.DefaultRequestHeaders.Add("BM-AUTH-APIKEY", _btcMarketSettings.ApiKey);
            client.DefaultRequestHeaders.Add("BM-AUTH-TIMESTAMP", now.ToString());
            client.DefaultRequestHeaders.Add("BM-AUTH-SIGNATURE", signature);
        }

        private string SignMessage(string message)
        {
            var bytes = Encoding.UTF8.GetBytes(message);

            using (var hash = new HMACSHA512(Convert.FromBase64String(_btcMarketSettings.PrivateKey)))
            {
                var hashedInputeBytes = hash.ComputeHash(bytes);
                return Convert.ToBase64String(hashedInputeBytes);
            }
        }
    }
}