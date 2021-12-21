using System;

namespace WillsAutoBot.Data.Entities
{
    public class CoinPriceEntity : BaseEntity
    {
        private string _coinId;
        private string _coinPriceId;
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

        public string CoinPriceId
        {
            get => _coinPriceId;
            set
            {
                _coinPriceId = value;
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

        public double Ask { get; set; }
        public double Bid { get; set; }
        public double Rate { get; set; }
        public string Sell { get; set; }

        public string Buy { get; set; }
        public double SpotRate { get; set; }
        public string Market { get; set; }
        public DateTime? TimestampUtc { get; set; }
        public string RateType { get; set; }
        public string RateSteps { get; set; }

        protected override void SetPartitionAndRowKeys()
        {
            PartitionKey = GetPartitionKey();
            RowKey = CoinPriceId;
        }

        protected override string GetPartitionKey()
        {
            return $"{CoinName}|{CoinId}";
        }
    }
}