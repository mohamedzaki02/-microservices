using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.Orders.Core.Entities;
using Microservices.Orders.Core.Repositories.Base;

namespace Microservices.Orders.Core.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string username);
    }
}