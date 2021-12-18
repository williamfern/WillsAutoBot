using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WillsAutoBot.BTCMarket.Services.BTCMarketDayTrading.Models;
using WillsAutoBot.BTCMarket.Services.BTCMarketDayTrading.Models.V3;
using WillsAutoBot.Settings;
using Order = WillsAutoBot.BTCMarket.Services.BTCMarketDayTrading.Models.V3.Order;

namespace WillsAutoBot.BTCMarket.Services.BTCMarketDayTrading
{
    public class BtcMarketClient : IBtcMarketClient
    {
        private readonly System.Net.Http.HttpClient client;
        private readonly IOptions<BtcMarketSettings> _options;

        public BtcMarketClient(System.Net.Http.HttpClient client, IOptions<BtcMarketSettings> options)
        {
            this.client = client;
            _options = options;
        }
        public async Task<TradingFee> GetTradingFee()
        {
            var endpoint = "/account/BTC/AUD/tradingfee";
            string signature = CreateSignature(endpoint);
            client.DefaultRequestHeaders.Add("signature", signature);
            var response =  await client.GetAsync(endpoint);
            var content = await response.Content.ReadAsAsync<TradingFee>();
            return content;
        }

        public async Task<Transactions> GetTransaction()
        {
            var endpoint = $"/v2/transaction/history";
            string signature = CreateSignature(endpoint);
            client.DefaultRequestHeaders.Add("signature", signature);
            var response = await client.GetAsync(endpoint);
            var content = await response.Content.ReadFromJsonAsync<Transactions>();
            return content;
        }

        public async Task<Transactions> GetTransaction(string currency)
        {
            var endpoint = $"/v2/transaction/history/{currency}";
            string signature = CreateSignature(endpoint);
            client.DefaultRequestHeaders.Add("signature", signature);
            var response = await client.GetAsync(endpoint);
            var content = await response.Content.ReadFromJsonAsync<Transactions>();
            return content;
        }

        public async Task<OrderResponse> GetOrderHistory(OrderRequest order)
        {
            var endpoint = $"/order/history";
            var jsonString = JsonConvert.SerializeObject(order);
            string signature = CreateSignaturePost(endpoint, jsonString);
            client.DefaultRequestHeaders.Add("signature", signature);
            var response = await client.PostAsync<OrderRequest>(endpoint, order,new JsonMediaTypeFormatter());
            var content = await response.Content.ReadFromJsonAsync<OrderResponse>();
            return content;
        }
        
        public async Task<Order> ListOrders(string marketId, string status)
        {
            var endpoint = $"/v3/orders";
            string signature = CreateSignature(endpoint);
            client.DefaultRequestHeaders.Add("signature", signature);
            var response = await client.GetAsync(endpoint);
            var content = await response.Content.ReadFromJsonAsync<Order>();
            return content;
        }
        
        public async Task<string> GetTime()
        {
            var endpoint = $"/v3/time";
            string signature = CreateSignature(endpoint);
            client.DefaultRequestHeaders.Add("signature", signature);
            var response = await client.GetAsync(endpoint);
            var content = await response.Content.ReadFromJsonAsync<TimeResponse>();
            return content?.Timestamp;
        }
        
        private string CreateSignature(string endpoint)
        {
            var data = $"{endpoint}\n{client.DefaultRequestHeaders.GetValues("timestamp").First()}\n";
            var encoding = Encoding.UTF8;
            using (var hasher = new HMACSHA512(Convert.FromBase64String(_options.Value.PrivateKey)))
            {
                return Convert.ToBase64String(hasher.ComputeHash(encoding.GetBytes(data)));
            }
        }

        private string CreateSignaturePost(string endpoint,string content)
        {
            var data = $"{endpoint}\n{client.DefaultRequestHeaders.GetValues("timestamp").First()}\n{content}";
            Console.WriteLine(data);
            var encoding = Encoding.UTF8;
            using (var hasher = new HMACSHA512(Convert.FromBase64String(_options.Value.PrivateKey)))
            {
                return Convert.ToBase64String(hasher.ComputeHash(encoding.GetBytes(data)));
            }
        }
    }
}
