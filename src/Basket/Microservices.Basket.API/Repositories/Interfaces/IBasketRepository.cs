using System.Threading.Tasks;
using Microservices.Basket.API.Entities;

namespace Microservices.Basket.API.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        Task<BasketCart> GetUserBasket(string username);
        Task<BasketCart> UpdateUserBasket(BasketCart basket);

        Task<bool> DeleteUserBasket(string username);
    }
}