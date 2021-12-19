using System.Threading.Tasks;
using WillsAutoBot.Cointree.Services.Models;

namespace WillsAutoBot.Cointree.Services.HttpClient
{
    public interface ICointreeHttpClient
    {
        public Task<CoinPriceResponse> GetCoinPrice(string coinName);
    }
}