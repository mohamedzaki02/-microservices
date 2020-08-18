using Microservices.Basket.API.Data.Interfaces;
using StackExchange.Redis;

namespace Microservices.Basket.API.Data
{
    public class BasketContext : IBasketContext
    {
        private readonly ConnectionMultiplexer _redisConnection;

        public BasketContext(ConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
            Redis = _redisConnection.GetDatabase();
        }
        public IDatabase Redis { get; }
    }
}