using System.Threading.Tasks;
using MediatR;
using Microservices.Orders.Application.Commands;
using Microservices.Orders.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Orders.API.Controllers
{

    [Route("api/v1/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpGet]
        public async Task<ActionResult> GetUserOrders(string username)
        {
            var query = new OrdersByUsernameQuery(username);
            return Ok(await _mediator.Send(query));
        }


        [HttpPost]
        public async Task<ActionResult> CheckoutOrder([FromBody] BasketCheckoutCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}