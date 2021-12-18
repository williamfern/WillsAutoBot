using System;

namespace WillsAutoBot.Data.Entities
{
    public class CoinPriceEntity : BaseEntity
    {
        private string _coinId;
        private string _coinName;

        private string CoinId
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
        
        public string Sell { get; set; }
        public string Buy { get; set; }
        public decimal SpotRate { get; set; }
        public string Market { get; set; }
        public DateTime? TimestampUtc { get; set; }
        public string RateType { get; set; }
        public string RateSteps { get; set; }

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
