using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Extensions.Logging;
using WillsAutoBot.Crypto.Services;
using WillsAutoBot.Utilities.Extensions;
using WillsAutoBot.WebApi.Models;

namespace WillsAutoBot.WebApi.Controllers
{
    [ApiController]
    [Route("markets")]
    public class MarketsController : ControllerBase
    {
        private readonly IMarketsService _marketService;

        //private readonly IMapper _mapper;
        private readonly ILogger<IMarketsService> _logger;

        public MarketsController(IMarketsService marketService, 
            ILogger<IMarketsService> logger)
        {
            _logger = logger;
            _marketService = marketService.ThrowIfNullOrDefault(nameof(marketService));
        }

        /// <summary>
        /// Gets and downloads the cloud template to setup an account for inclusion in TCS.
        /// </summary>
        /// <response code="200">The orders have been retrieved.</response>
        /// <response code="400">The action is invalid.</response>
        /// <response code="404">The orders not found.</response>
        [HttpGet]
        [Route("listActiveMarkets", Name = nameof(listActiveMarkets))]
        [ProducesResponseType(typeof(OrdersApiModel), 200)]
        [ProducesResponseType(typeof(Error[]), 400)]
        [ProducesResponseType(typeof(Error[]), 404)]
        public async Task<ActionResult> listActiveMarkets()
        {
            // var result1 = await _ordersService.GetOrder("7346816281");
            await _marketService.GetActiveMarket();

            return Ok();
        }
    }
}