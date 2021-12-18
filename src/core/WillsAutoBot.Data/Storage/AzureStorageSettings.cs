namespace WillsAutoBot.Data.Storage
{
    public class AzureStorageSettings
    {
        public AzureStorageSettings(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }
    }
}
