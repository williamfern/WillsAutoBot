using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillsAutoBot.BTCMarket.Services.Constants.Models;
using WillsAutoBot.BTCMarket.Services.Models;
using WillsAutoBot.Services.Models;

namespace WillsAutoBot.BTCMarket.Services.HttpClient
{
    public interface IBtcMarketHttpClient
    {
        Task<ResponseModel> Get(string path, string queryString);

        Task<string> Post(string path, string queryString, object data);

        Task<string> Put(string path, string queryString, object data);

        Task<string> Delete(string path, string queryString);

    }
}
