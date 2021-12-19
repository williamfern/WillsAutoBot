using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillsAutoBot.Utilities.Extensions;
using Microsoft.Azure.Cosmos.Table;

namespace WillsAutoBot.Data.Storage
{
    public abstract class StorageTableRepository<T> : AzureStorage
        where T : TableEntity, new()
    {
        protected const string PartitionKeyPropertyName = "PartitionKey";
        protected const string RowKeyPropertyName = "RowKey";

        private readonly string _tableName;
        private static CloudTableClient _storageTableClient;

        protected StorageTableRepository(string connectionString, string tableName)
            : base(connectionString)
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException(nameof(tableName));
            _tableName = tableName;

            //Account  
            var storageAccount = GetTableStorageAccount();

            //Client  
            _storageTableClient = storageAccount.CreateCloudTableClient();
            _storageTableClient.DefaultRequestOptions = new TableRequestOptions
            {
                RetryPolicy = new ExponentialRetry(TimeSpan.FromMilliseconds(10), 3),
                PayloadFormat = TablePayloadFormat.JsonNoMetadata
            };
        }

        protected async Task<CloudTable> GetTable()
        {
            var table = _storageTableClient.GetTableReference(_tableName);
            await table.SafelyCreateIfNotExistsAsync();

            return table;
        }

        protected virtual async Task<T> Get(string partitionKey, string rowKey)
        {
            partitionKey.ThrowIfNullOrWhiteSpace($"{_tableName}:{nameof(partitionKey)}");
            rowKey.ThrowIfNullOrWhiteSpace($"{_tableName}:{nameof(rowKey)}");

            //Table  
            var table = await GetTable();

            //Query
            var retrieve = await table.ExecuteAsync(TableOperation.Retrieve<T>(partitionKey, rowKey));

            if (!(retrieve.Result is T entity))
                return null;

            return entity;
        }

        protected async Task<bool> EntityExists(string partitionKey, string rowKey)
        {
            partitionKey.ThrowIfNullOrWhiteSpace($"{_tableName}:{nameof(partitionKey)}");
            rowKey.ThrowIfNullOrWhiteSpace($"{_tableName}:{nameof(rowKey)}");

            //Table  
            var table = await GetTable();

            var tableQuery = new TableQuery<T>
            {
                FilterString = TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition(PartitionKeyPropertyName, QueryComparisons.Equal, partitionKey),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition(RowKeyPropertyName, QueryComparisons.Equal, rowKey))
            };

            var dynamicTableEntities = await table.ExecuteQuerySegmentedAsync(tableQuery, null);
            return dynamicTableEntities.Results.Any();
        }

        protected Task<IEnumerable<T>> GetAll()
        {
            return GetFiltered();
        }

        protected async Task<IEnumerable<T>> GetAll(string partitionKey)
        {
            partitionKey.ThrowIfNullOrWhiteSpace($"{_tableName}:{nameof(partitionKey)}");

            return await GetFiltered(partitionKey);
        }

        protected async Task<(IEnumerable<T> results, string nextContinuationToken)> GetAll(string partitionKey,
            int pageSize, string continuationToken)
        {
            partitionKey.ThrowIfNullOrWhiteSpace($"{_tableName}:{nameof(partitionKey)}");

            return await GetFiltered(partitionKey, pageSize, string.Empty, continuationToken);
        }

        protected async Task<IEnumerable<T>> GetPartitionKeyStartsWith(string startsWith)
        {
            return await GetFiltered(string.Empty,
                GetPropertyStartsWithFilterString(PartitionKeyPropertyName, startsWith));
        }

        protected async Task<IEnumerable<T>> GetFiltered(string partitionKey = "", string filterQuery = "")
        {
            //Table  
            var table = await GetTable();

            //Query  
            var tableQuery = new TableQuery<T>();

            if (!partitionKey.IsNullOrWhiteSpace())
            {
                tableQuery =
                    tableQuery.Where(TableQuery.GenerateFilterCondition(PartitionKeyPropertyName,
                        QueryComparisons.Equal,
                        partitionKey));
            }

            if (!filterQuery.IsNullOrWhiteSpace())
            {
                if (partitionKey.IsNullOrWhiteSpace())
                {
                    tableQuery.FilterString = filterQuery;
                }
                else
                {
                    tableQuery.FilterString = TableQuery.CombineFilters(
                        tableQuery.FilterString,
                        TableOperators.And,
                        filterQuery);
                }
            }

            var results = new List<T>();
            TableContinuationToken continuationToken = null;
            do
            {
                var queryResults =
                    await table.ExecuteQuerySegmentedAsync<T>(tableQuery, continuationToken);

                continuationToken = queryResults.ContinuationToken;
                results.AddRange(queryResults.Results);
            } while (continuationToken != null);

            return results;
        }

        protected async Task<(IEnumerable<T> results, string nextContinuationToken)> GetFiltered(string partitionKey,
            int pageSize, string filterQuery = "",
            string continuationToken = null)
        {
            partitionKey.ThrowIfNullOrWhiteSpace($"{_tableName}:{nameof(partitionKey)}");

            //Table  
            var table = await GetTable();

            //Query  
            var tableQuery = new TableQuery<T>().Where(
                TableQuery.GenerateFilterCondition(PartitionKeyPropertyName, QueryComparisons.Equal, partitionKey));

            tableQuery.TakeCount = pageSize;

            if (!filterQuery.IsNullOrWhiteSpace())
            {
                tableQuery.FilterString = TableQuery.CombineFilters(
                    tableQuery.FilterString,
                    TableOperators.And,
                    filterQuery);
            }

            TableContinuationToken tableStorageContinuationToken = null;
            if (continuationToken != null)
            {
                tableStorageContinuationToken = new TableContinuationToken
                {
                    NextPartitionKey = partitionKey,
                    NextRowKey = continuationToken,
                    NextTableName = table.Name
                };
            }

            var queryResults = await table.ExecuteQuerySegmentedAsync<T>(tableQuery, tableStorageContinuationToken);
            var results = new List<T>(queryResults.Results);
            var nextContinuationToken = queryResults.ContinuationToken?.NextRowKey;

            return (results, nextContinuationToken);
        }

        protected async Task<IEnumerable<string>> GetRowKeys(string partitionKey)
        {
            var entities = await GetSelectedFields(partitionKey, new[] { RowKeyPropertyName });
            return entities.ToList().Select(e => e.RowKey);
        }

        /// <summary>
        /// Only loads the Column Names mentioned
        /// </summary>
        /// <param name="partitionKey"></param>
        /// <param name="columns"></param>
        /// <param name="filter"></param>
        /// <returns>A task containing a list of dynamic table entities with the specified fields included.</returns>
        private async Task<IList<DynamicTableEntity>> GetSelectedFields(string partitionKey, string[] columns,
            string filter = "")
        {
            partitionKey.ThrowIfNullOrWhiteSpace($"{_tableName}:{nameof(partitionKey)}");
            columns.ThrowIfNullOrDefault($"{_tableName}:{nameof(columns)}");

            //Table  
            var table = await GetTable();

            var tableQuery = new TableQuery().Where(TableQuery.GenerateFilterCondition(PartitionKeyPropertyName,
                QueryComparisons.Equal, partitionKey));

            if (!filter.IsNullOrWhiteSpace())
            {
                var filterQuery =
                    TableQuery.GenerateFilterCondition(RowKeyPropertyName, QueryComparisons.GreaterThanOrEqual, filter);

                tableQuery.FilterString = TableQuery.CombineFilters(
                    tableQuery.FilterString,
                    TableOperators.And,
                    filterQuery);
            }

            tableQuery = tableQuery.Select(columns);

            var results = new List<DynamicTableEntity>();
            TableContinuationToken continuationToken = null;

            // Get All Results
            do
            {
                var queryResults = await table.ExecuteQuerySegmentedAsync(tableQuery, continuationToken);
                continuationToken = queryResults.ContinuationToken;
                results.AddRange(queryResults.Results);
            } while (continuationToken != null);

            return results;
        }

        protected virtual async Task<T> Insert(T entity)
        {
            entity.ThrowIfNullOrDefault($"{_tableName}:{nameof(entity)}");

            //Validate
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(entity, new ValidationContext(entity), results, true);
            if (!isValid)
            {
                StringBuilder errorMessage = new StringBuilder();
                foreach (var validationResult in results)
                {
                    errorMessage.AppendLine(validationResult.ErrorMessage);
                }

                throw new ArgumentException(errorMessage.ToString());
            }

            //Table
            var table = await GetTable();

            //Operation
            var operation = TableOperation.Insert(entity);

            //Execute
            await table.ExecuteAsync(operation);

            return entity;
        }

        protected virtual async Task<T> InsertOrUpdate(T entity)
        {
            entity.ThrowIfNullOrDefault($"{_tableName}:{nameof(entity)}");

            //Validate
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(entity, new ValidationContext(entity), results, true);
            if (!isValid)
            {
                StringBuilder errorMessage = new StringBuilder();
                foreach (var validationResult in results)
                {
                    errorMessage.AppendLine(validationResult.ErrorMessage);
                }

                throw new ArgumentException(errorMessage.ToString());
            }

            //Table  
            var table = await GetTable();

            //Operation  
            var operation = TableOperation.InsertOrReplace(entity);

            //Execute  
            await table.ExecuteAsync(operation);

            return entity;
        }

        protected async Task InsertBatch(IEnumerable<T> entities, bool shallMerge = false)
        {
            //Validate
            foreach (var entity in entities)
            {
                var results = new List<ValidationResult>();
                var isValid = Validator.TryValidateObject(entity, new ValidationContext(entity), results, true);
                if (!isValid)
                {
                    StringBuilder errorMessage = new StringBuilder();
                    foreach (var validationResult in results)
                    {
                        errorMessage.AppendLine(validationResult.ErrorMessage);
                    }

                    throw new ArgumentException(errorMessage.ToString());
                }
            }

            //Table  
            var table = await GetTable();

            int rowOffset = 0;
            while (rowOffset < entities.ToList().Count)
            {
                var batch = new TableBatchOperation();

                // next batch
                var rows = entities.Skip(rowOffset).Take(100).ToList();
                foreach (var row in rows)
                {
                    if (shallMerge)
                        batch.Add(TableOperation.InsertOrMerge(row));
                    else
                        batch.Add(TableOperation.InsertOrReplace(row));
                }

                await table.ExecuteBatchAsync(batch);

                rowOffset += rows.Count;
            }
        }

        protected async Task Delete(string partitionKey, string rowKey)
        {
            //Table  
            var table = await GetTable();

            var entity = await Get(partitionKey, rowKey);

            if (!entity.IsNullOrDefault())
            {
                //Operation  
                var operation = TableOperation.Delete(entity);

                //Execute  
                await table.ExecuteAsync(operation);
            }
        }

        protected async Task DeleteBatch(string partitionKey)
        {
            //Table  
            var table = await GetTable();

            var entities = await GetSelectedFields(partitionKey, new[] { RowKeyPropertyName });

            var offset = 0;
            while (offset < entities.Count)
            {
                var batch = new TableBatchOperation();
                var rows = entities.Skip(offset).Take(100).ToList();
                foreach (var row in rows)
                {
                    batch.Delete(row);
                }

                await table.ExecuteBatchAsync(batch);
                offset += rows.Count;
            }
        }

        protected string GetPropertyStartsWithFilterString(string propertyName, string startsWith)
        {
            propertyName.ThrowIfNullOrWhiteSpace($"{_tableName}:{nameof(propertyName)}");
            startsWith.ThrowIfNullOrWhiteSpace($"{_tableName}:{nameof(startsWith)}");

            var length = startsWith.Length - 1;
            var nextChar = startsWith[length] + 1;

            var partitionKeyStartsWithEnd = startsWith.Substring(0, length) + (char)nextChar;
            return TableQuery.CombineFilters(
                TableQuery.GenerateFilterCondition(propertyName, QueryComparisons.GreaterThanOrEqual, startsWith),
                TableOperators.And,
                TableQuery.GenerateFilterCondition(propertyName, QueryComparisons.LessThan, partitionKeyStartsWithEnd));
        }
    }
}