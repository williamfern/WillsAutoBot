namespace WillsAutoBot.Data.Entities
{
    public class UserEntity : BaseEntity
    {
        private string _userId;

        private string UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                SetPartitionAndRowKeys();
            }
        }

        public string UserPreferredCoin { get; set; }

        protected override void SetPartitionAndRowKeys()
        {
            PartitionKey = GetPartitionKey();
            RowKey = UserId;
        }

        protected override string GetPartitionKey()
        {
            return UserId;
        }
    }
}