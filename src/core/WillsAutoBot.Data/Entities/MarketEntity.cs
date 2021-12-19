namespace WillsAutoBot.Data.Entities
{
    public class MarketEntity : BaseEntity
    {
        private string _wabId;
        private string _marketId;

        public string WabId
        {
            get => _wabId;
            set
            {
                _wabId = value;
                SetPartitionAndRowKeys();
            }
        }

        public string MarketId
        {
            get => _marketId;
            set
            {
                _marketId = value;
                SetPartitionAndRowKeys();
            }
        }

        public string BestBid { get; set; }
        public string BestAsk { get; set; }
        public string LastPrice { get; set; }
        public string Volume24h { get; set; }
        public string Price24h { get; set; }
        public string Low24h { get; set; }
        public string High24h { get; set; }

        protected override void SetPartitionAndRowKeys()
        {
            PartitionKey = GetPartitionKey();
            RowKey = MarketId;
        }

        protected override string GetPartitionKey()
        {
            return $"{WabId}|{MarketId}";
        }
    }
}