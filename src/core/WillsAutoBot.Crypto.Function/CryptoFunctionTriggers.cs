using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using WillsAutoBot.Crypto.Services;
using WillsAutoBot.Utilities.Extensions;

namespace WillsAutoBot.Crypto.Function
{
    public class CryptoFunctionTriggers
    {
        private readonly ICoinService _coinService;

        public CryptoFunctionTriggers(ICoinService coinService)
        {
            _coinService = coinService.ThrowIfNullOrDefault(nameof(coinService));
        }

        [FunctionName(nameof(ProcessCoinPrice))]
        public async Task ProcessCoinPrice(
            [TimerTrigger("0 * * * * *", RunOnStartup = true)]
            TimerInfo timer,
            ILogger functionsLogger)
        {
            functionsLogger.LogInformation("Running coin price every hour..");
            await _coinService.ProcessCoinPriceList();
        }
    }
}