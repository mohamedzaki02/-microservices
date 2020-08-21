using System;
using System.Threading.Tasks;
using AutoMapper;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producers;
using Microservices.Basket.API.Entities;
using Microservices.Basket.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Basket.API.Controllers
{

    [Route("api/v1/basket")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repo;
        private readonly IMapper _mapper;
        private readonly RabbitMQProducer _eventBus;

        public BasketController(IBasketRepository repo, IMapper mapper, RabbitMQProducer eventBus)
        {
            _eventBus = eventBus;
            _mapper = mapper;
            _repo = repo;
        }



        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserBasket(string username)
        {
            var basket = await _repo.GetUserBasket(username);
            return Ok(basket ?? new BasketCart(username));
        }


        [HttpPost]
        public async Task<IActionResult> UpdateUserBasket([FromBody] BasketCart basket)
        {
            var result = await _repo.UpdateUserBasket(basket);
            if (result == null) return BadRequest("Could Not Update User Basket");
            return NoContent();
        }


        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteUserBasket(string username)
        {
            var result = await _repo.DeleteUserBasket(username);
            if (result == false) return BadRequest("Could Not Delete User Basket");
            return NoContent();
        }


        [Route("Checkout")]
        [HttpPost]
        public async Task<IActionResult> Checkout(BasketCheckout basketCheckot)
        {
            var basket = await _repo.GetUserBasket(basketCheckot.UserName);
            if (basket == null) return NotFound("Could Not Find User Basket");

            var basketDeleted = await _repo.DeleteUserBasket(basketCheckot.UserName);
            if (!basketDeleted) return BadRequest("Coud not Delete User Basket");

            var basketEvent = _mapper.Map<BasketCheckoutEvent>(basketCheckot);
            basketEvent.RequestId = Guid.NewGuid();
            basketEvent.TotalPrice = basket.TotalPrice;

            _eventBus.PublishBasketCheckout(EventBusConstants.BasketCheckoutQueue, basketEvent);

            return Accepted();
        }


    }
}