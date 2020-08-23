using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microservices.Orders.Application.Profiles.Orders;
using Microservices.Orders.Application.Queries;
using Microservices.Orders.Application.Responses;
using Microservices.Orders.Core.Repositories;

namespace Microservices.Orders.Application.QueryHandlers
{
    public class OrdersByUsernameQueryHandler : IRequestHandler<OrdersByUsernameQuery, IEnumerable<OrderResponse>>
    {
        private readonly IOrderRepository _repo;
        public OrdersByUsernameQueryHandler(IOrderRepository orderRepository)
        {
            _repo = orderRepository;

        }
        public async Task<IEnumerable<OrderResponse>> Handle(OrdersByUsernameQuery request, CancellationToken cancellationToken)
        {
            return OrderMapper.Mapper.Map<IEnumerable<OrderResponse>>(await _repo.GetOrdersByUserName(request.Username));
        }
    }
}