using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WillsAutoBot.BTCMarket.Services.Constants;
using WillsAutoBot.BTCMarket.Services.HttpClient;
using WillsAutoBot.Utilities.Extensions;
using Order = WillsAutoBot.Services.Models.Order;

namespace WillsAutoBot.Crypto.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IBtcMarketHttpClient _btcMarketHttpClient;
        private readonly ILogger<IOrdersService> _logger;

        public OrdersService(
            IBtcMarketHttpClient btcMarketHttpClient, 
            ILogger<IOrdersService> logger)
        {
            _btcMarketHttpClient = btcMarketHttpClient.ThrowIfNullOrDefault(nameof(btcMarketHttpClient));
            _logger = logger.ThrowIfNullOrDefault(nameof(logger));
        }

        public async Task<List<Order>> GetAllOrders()
        {
            var response = await _btcMarketHttpClient.Get(VersionConstants.Version3 + MethodConstants.ORDERS, "status=all&limit=200");

            var res = JsonSerializer.Deserialize<List<Order>>(response.Content);

            return res;
        }
    }
}