using StackExchange.Redis;

namespace Microservices.Basket.API.Data.Interfaces
{
    public interface IBasketContext
    {
        IDatabase Redis { get; }
    }
}