using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillsAutoBot.Data.Entities;

namespace WillsAutoBot.Data.Repositories.Market
{
    public interface IMarketRepository
    {
        /// <summary>
        /// Adds a market.
        /// </summary>
        /// <param name="market">The market to add.</param>
        /// <returns>A task.</returns>
        Task Add(MarketEntity market);

        /// <summary>
        /// Adds or updates the market record.
        /// </summary>
        /// <param name="market">The market to be added or updated.</param>
        /// <returns>A task.</returns>
        Task AddOrUpdate(MarketEntity market);
    }
}
