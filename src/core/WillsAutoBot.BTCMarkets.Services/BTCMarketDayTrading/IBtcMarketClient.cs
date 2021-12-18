using System.Threading.Tasks;
using WillsAutoBot.BTCMarket.Services.BTCMarketDayTrading.Models;
using Order = WillsAutoBot.BTCMarket.Services.BTCMarketDayTrading.Models.V3.Order;

namespace WillsAutoBot.BTCMarket.Services.BTCMarketDayTrading
{
    public interface IBtcMarketClient
    {
        Task<TradingFee> GetTradingFee();
        Task<Transactions> GetTransaction();
        Task<Transactions> GetTransaction(string currency);
        Task<OrderResponse> GetOrderHistory(OrderRequest order);
        Task<Order> ListOrders(string marketId, string status);
        Task<string> GetTime();
        
        
    }
}
