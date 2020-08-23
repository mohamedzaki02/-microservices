using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.Orders.Core.Entities;
using Microservices.Orders.Core.Repositories;
using Microservices.Orders.Infra.Data;
using Microservices.Orders.Infra.Repositories.Base;

namespace Microservices.Orders.Infra.Repositories
{
    public class OrdersRepository : Repository<Order>, IOrderRepository
    {
        private readonly OrdersContext _ctx;
        public OrdersRepository(OrdersContext ordersContext) : base(ordersContext)
        {
            _ctx = ordersContext;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string username)
        {
            return await GetAsync(o => o.UserName.ToLower().Contains(username.ToLower()));
        }
    }
}