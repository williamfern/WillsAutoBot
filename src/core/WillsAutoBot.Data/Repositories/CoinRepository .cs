using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WillsAutoBot.Data.Entities;
using WillsAutoBot.Data.Storage;
using WillsAutoBot.Settings;

namespace WillsAutoBot.Data.Repositories
{
    public class CoinPriceRepository : StorageTableRepository<CoinPriceEntity>, ICoinPriceRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoinPriceRepository"/> class.
        /// </summary>
        /// <param name="storageSettings">The Azure storage settings.</param>
        /// <param name="tableNameSettings">The Azure table name settings.</param>
        public CoinPriceRepository(AzureStorageSettings storageSettings, IOptions<TableNameSettings> tableNameSettings)
            : base(storageSettings.ConnectionString, tableNameSettings.Value.CoinPriceTableName)
        {
        }

        public Task Add(CoinPriceEntity coin)
            => Insert(coin);

        public Task AddOrUpdate(CoinPriceEntity coin)
            => InsertOrUpdate(coin);
    }
}