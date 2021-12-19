using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WillsAutoBot.Data.Entities;
using WillsAutoBot.Data.Storage;
using WillsAutoBot.Settings;

namespace WillsAutoBot.Data.Repositories
{
    public class CoinRepository : StorageTableRepository<CoinEntity>, ICoinRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoinRepository"/> class.
        /// </summary>
        /// <param name="storageSettings">The Azure storage settings.</param>
        /// <param name="tableNameSettings">The Azure table name settings.</param>
        public CoinRepository(AzureStorageSettings storageSettings, IOptions<TableNameSettings> tableNameSettings)
            : base(storageSettings.ConnectionString, tableNameSettings.Value.CoinTableName)
        {
        }

        public Task Add(CoinEntity coin)
            => Insert(coin);

        public Task AddOrUpdate(CoinEntity coin)
            => InsertOrUpdate(coin);
    }
}