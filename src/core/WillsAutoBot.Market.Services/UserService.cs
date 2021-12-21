using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WillsAutoBot.Services.Models;
using WillsAutoBot.Utilities.Extensions;

namespace WillsAutoBot.Crypto.Services
{
    public class UserService : IUserService
    {
        private UserPreference _currentUserPref;
        private readonly ICoinService _coinService;
        private readonly ILogger<IUserService> _logger;

        public UserService(ICoinService coinService, ILogger<IUserService> logger)
        {
            _coinService = coinService.ThrowIfNullOrDefault(nameof(coinService));
            _logger = logger.ThrowIfNullOrDefault(nameof(logger));
        }

        public void WithUserPreference(UserPreference userPref)
        {
            _currentUserPref = userPref;
        }

        public UserPreference GetUserPreference()
        {
            return _currentUserPref;
        }

        public async Task<string> GetUserPreferredCoin()
        {
            if (_currentUserPref != null)
            {
                return _currentUserPref.PreferredCoin;
            }
            else
            {
                var coinList = await _coinService.FindAllCoins();
                var defaultCoin = coinList.FirstOrDefault(c => c.IsDefault);

                return defaultCoin?.CoinName;
            }
        }
    }
}