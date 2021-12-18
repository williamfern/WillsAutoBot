using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WillsAutoBot.Crypto.Services;
using WillsAutoBot.Services.Models;
using WillsAutoBot.Utilities.Extensions;
using WillsAutoBot.WebApi.Models;

namespace WillsAutoBot.WebApi.Controllers
{
    [ApiController]
    [Route("coin")]
    public class CoinController : Controller
    {
        private readonly ICoinService _coinService;
        private readonly IMapper _mapper;
        
        public CoinController(ICoinService coinService, IMapper mapper)
        {
            _coinService = coinService.ThrowIfNullOrDefault(nameof(coinService));
            _mapper = mapper.ThrowIfNullOrDefault(nameof(mapper));
        }
        
        /// <summary>
        /// Supply a list of coins avaiable for the client
        /// </summary>
        /// <returns></returns>
        [Route("GetAllCoins")]
        [HttpGet]
        public async Task<ActionResult> GetAllCoins()
        {
           return Ok(_mapper.Map<IEnumerable<CoinApiModel>>(await _coinService.GetCoinList()));
        }
        
        
        // /// <summary>
        // /// Set user preference. Currently only accept the coin symbol
        // /// </summary>
        // /// <param name="userPref"></param>
        // /// <returns></returns>
        // [Route("SetUserPreferences")]
        // [EnableCors("AllowEveryThing")]
        // [HttpPost]
        // public string SetUserPreferences([FromBody] UserPreference userPref)
        // {
        //     userPrefService.SetUserPreference(userPref);
        //     var currentUserPref = userPrefService.GetUserPreference();
        //
        //     if (currentUserPref == null)
        //         return "";
        //
        //     return userPrefService.GetUserPreference().PreferredCoin;
        // }
        //
        //
        // /// <summary>
        // /// Retrieve current coin price details
        // /// </summary>
        // /// <returns></returns>
        // [Route("EnquireCoinPriceDetails")]
        // [EnableCors("AllowEveryThing")]
        // [HttpGet]
        // public async Task<PriceEnquiryResponse> EnquireCoinPriceDetails()
        // {
        //     return await priceService.GetCoinPriceDetails();
        // }

    }
}