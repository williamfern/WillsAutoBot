namespace WillsAutoBot.Data.Entities
{
    public class CoinEntity : BaseEntity
    {
        private string _coinId;
        private string _coinName;

        public string CoinId
        {
            get => _coinId;
            set
            {
                _coinId = value;
                SetPartitionAndRowKeys();
            }
        }

        public string CoinName
        {
            get => _coinName;
            set
            {
                _coinName = value;
                SetPartitionAndRowKeys();
            }
        }
        public bool IsDefault { get; set; }

        protected override void SetPartitionAndRowKeys()
        {
            PartitionKey = GetPartitionKey();
            RowKey = CoinId;
        }

        protected override string GetPartitionKey()
        {
            return CoinId;
        }
    }
}
