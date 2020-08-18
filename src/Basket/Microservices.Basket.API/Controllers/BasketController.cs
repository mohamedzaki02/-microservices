using System.Threading.Tasks;
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

        public BasketController(IBasketRepository repo)
        {
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

    }
}