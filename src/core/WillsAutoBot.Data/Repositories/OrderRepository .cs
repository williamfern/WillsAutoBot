using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WillsAutoBot.Data.Entities;
using WillsAutoBot.Data.Storage;
using WillsAutoBot.Settings;

namespace WillsAutoBot.Data.Repositories.Order
{
    public class OrderRepository : StorageTableRepository<OrderEntity>, IOrderRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRepository"/> class.
        /// </summary>
        /// <param name="storageSettings">The Azure storage settings.</param>
        /// <param name="tableNameSettings">The Azure table name settings.</param>
        public OrderRepository(AzureStorageSettings storageSettings, IOptions<TableNameSettings> tableNameSettings)
            : base(storageSettings.ConnectionString, tableNameSettings.Value.MarketTableName)
        {
        }

        public Task Add(OrderEntity order)
            => Insert(order);

        public Task AddOrUpdate(OrderEntity order)
            => InsertOrUpdate(order);
    }
}