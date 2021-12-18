using System.Collections.Generic;
using System.Threading.Tasks;
using WillsAutoBot.BTCMarket.Services.BTCMarketDayTrading.Models;
using Order = WillsAutoBot.Services.Models.Order;

namespace WillsAutoBot.Crypto.Services
{
    /// <inheritdoc />
    /// <summary>
    /// Implements the order service.
    /// </summary>
    public interface IOrdersService
    {
        /// <summary>
        /// Lists all orders that present for the user.
        /// </summary>
        /// <returns>A task containing an enumeration of accounts.</returns>
        Task<List<Order>> GetAllOrders();
        
    }
}
