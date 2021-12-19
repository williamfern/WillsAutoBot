using System.Collections.Generic;
using System.Threading.Tasks;
using WillsAutoBot.Services.Models;

namespace WillsAutoBot.Crypto.Services
{
    public interface IUserService
    {
        void WithUserPreference(UserPreference userPref);

        UserPreference GetUserPreference();

        Task<string> GetUserPreferredCoin();
    }
}