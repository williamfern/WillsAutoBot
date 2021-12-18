using Microsoft.Azure.Cosmos.Table;
using System;

namespace WillsAutoBot.Data.Storage
{
    public abstract class AzureStorage
    {
        private static CloudStorageAccount _storageAccount;
        private readonly string _connectionString;

        protected AzureStorage(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        protected virtual CloudStorageAccount GetTableStorageAccount()
        {
            return _storageAccount ?? (_storageAccount = CloudStorageAccount.Parse(_connectionString));
        }
    }
}
