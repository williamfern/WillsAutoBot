using System.Threading.Tasks;

namespace WillsAutoBot.Crypto.Services
{
    public interface IMarketsService
    {
        Task GetActiveMarket();
    }
}