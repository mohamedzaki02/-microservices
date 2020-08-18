using System.Threading.Tasks;
using Microservices.Basket.API.Data.Interfaces;
using Microservices.Basket.API.Entities;
using Microservices.Basket.API.Repositories.Interfaces;
using Newtonsoft.Json;

namespace Microservices.Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IBasketContext _context;

        public BasketRepository(IBasketContext context)
        {
            _context = context;
        }

        public async Task<BasketCart> GetUserBasket(string username)
        {
            var basket = await _context.Redis.StringGetAsync(username);
            if (basket.IsNullOrEmpty) return null;
            return JsonConvert.DeserializeObject<BasketCart>(basket);
        }

        public async Task<BasketCart> UpdateUserBasket(BasketCart basket)
        {
            var updated = await _context.Redis.StringSetAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            if (!updated) return null;
            return await GetUserBasket(basket.UserName);
        }

        public async Task<bool> DeleteUserBasket(string username)
        {
            return await _context.Redis.KeyDeleteAsync(username);
        }
    }
}