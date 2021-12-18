namespace WillsAutoBot.Crypto.Services
{
    public class MarketsService : IMarketsService
    {
        private MarketsService()
        {
        }

        public static IMarketsService Instance { get; } = new MarketsService();
    }
}
