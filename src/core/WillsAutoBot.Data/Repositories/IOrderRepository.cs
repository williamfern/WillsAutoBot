using System.Threading.Tasks;
using WillsAutoBot.Data.Entities;

namespace WillsAutoBot.Data.Repositories.Order
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Adds a market.
        /// </summary>
        /// <param name="order">The order to add.</param>
        /// <returns>A task.</returns>
        Task Add(OrderEntity order);

        /// <summary>
        /// Adds or updates the order record.
        /// </summary>
        /// <param name="order">The order to be added or updated.</param>
        /// <returns>A task.</returns>
        Task AddOrUpdate(OrderEntity order);
    }
}
