using System;

namespace WillsAutoBot.Data.Entities
{
    public class OrderEntity : BaseEntity
    {
        private string _orderId;
        private string _marketId;

        public string OrderId
        {
            get => _orderId;
            set
            {
                _orderId = value;
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

        public string Side { get; set; }
        public string Type { get; set; }
        public DateTime CreationTime { get; set; }
        public string Price { get; set; }
        public string Amount { get; set; }
        public string OpenAmount { get; set; }
        public string Status { get; set; }

        protected override void SetPartitionAndRowKeys()
        {
            PartitionKey = GetPartitionKey();
            RowKey = MarketId;
        }

        protected override string GetPartitionKey()
        {
            return $"{OrderId}";
        }
    }
}