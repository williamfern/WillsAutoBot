using System.Collections.Generic;
using System.Threading.Tasks;
using WillsAutoBot.Data.Entities;

namespace WillsAutoBot.Data.Repositories
{
    public interface ICoinRepository
    {
        /// <summary>
        /// Adds a coin.
        /// </summary>
        /// <param name="coin">The order to add.</param>
        /// <returns>A task.</returns>
        Task Add(CoinEntity coin);

        /// <summary>
        /// Adds or updates the coin record.
        /// </summary>
        /// <param name="coin">The coin to be added or updated.</param>
        /// <returns>A task.</returns>
        Task AddOrUpdate(CoinEntity coin);

        /// <summary>
        /// Finds all coins within the specified coin name.
        /// </summary>
        Task<IEnumerable<CoinEntity>> FindAll();
    }
}