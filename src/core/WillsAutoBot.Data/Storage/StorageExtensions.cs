using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.Azure.Cosmos.Table;

namespace WillsAutoBot.Data.Storage
{
    public static class StorageExtensions
    {
        public static async Task SafelyCreateIfNotExistsAsync(this CloudTable resource)
        {
            if (!await resource.ExistsAsync()) await resource.CreateAsync();
        }

        public static async Task SafelyCreateIfNotExistsAsync(this QueueClient resource)
        {
            if (!await resource.ExistsAsync()) await resource.CreateAsync();
        }

        public static async Task SafelyCreateIfNotExistsAsync(this BlobClient resource)
        {
            if (!await resource.ExistsAsync()) await resource.SafelyCreateIfNotExistsAsync();
        }
    }
}