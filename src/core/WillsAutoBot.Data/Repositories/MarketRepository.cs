using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WillsAutoBot.Data.Entities;
using WillsAutoBot.Data.Storage;
using WillsAutoBot.Settings;

namespace WillsAutoBot.Data.Repositories.Market
{
    public class MarketRepository : StorageTableRepository<MarketEntity>, IMarketRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarketRepository"/> class.
        /// </summary>
        /// <param name="storageSettings">The Azure storage settings.</param>
        /// <param name="tableNameSettings">The Azure table name settings.</param>
        public MarketRepository(AzureStorageSettings storageSettings, IOptions<TableNameSettings> tableNameSettings)
            : base(storageSettings.ConnectionString, tableNameSettings.Value.MarketTableName)
        {
        }

        public Task Add(MarketEntity market)
            => Insert(market);

        public Task AddOrUpdate(MarketEntity market)
            => InsertOrUpdate(market);
    }
}