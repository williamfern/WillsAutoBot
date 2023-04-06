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
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        //private readonly IMapper _mapper;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrdersService ordersService, ILogger<OrdersController> logger)
        {
            _logger = logger;
            _ordersService = ordersService.ThrowIfNullOrDefault(nameof(ordersService));
        }

        /// <summary>
        /// Gets and downloads the cloud template to setup an account for inclusion in TCS.
        /// </summary>
        /// <response code="200">The orders have been retrieved.</response>
        /// <response code="400">The action is invalid.</response>
        /// <response code="404">The orders not found.</response>
        [HttpGet]
        [Route("listOrders", Name = nameof(GetAllOrders))]
        [ProducesResponseType(typeof(OrdersApiModel), 200)]
        [ProducesResponseType(typeof(Error[]), 400)]
        [ProducesResponseType(typeof(Error[]), 404)]
        public async Task<ActionResult> GetAllOrders()
        {
            // var result1 = await _ordersService.GetOrder("7346816281");
            var result = await _ordersService.GetAllOrders();

            return Ok(result);
        }
    }
}