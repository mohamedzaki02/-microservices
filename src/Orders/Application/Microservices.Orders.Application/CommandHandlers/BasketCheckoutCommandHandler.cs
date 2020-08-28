using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microservices.Orders.Application.Commands;
using Microservices.Orders.Application.Profiles.Orders;
using Microservices.Orders.Application.Responses;
using Microservices.Orders.Core.Entities;
using Microservices.Orders.Core.Repositories;

namespace Microservices.Orders.Application.CommandHandlers
{
    public class BasketCheckoutCommandHandler : IRequestHandler<BasketCheckoutCommand, OrderResponse>
    {
        private readonly IOrderRepository _repo;
        public BasketCheckoutCommandHandler(IOrderRepository repo)
        {
            _repo = repo;

        }

        // JUST FOR TESTING BEFORE CREATING & REGISTERING RabbitMQ Listener
        public async Task<OrderResponse> Handle(BasketCheckoutCommand request, CancellationToken cancellationToken)
        {
            var order = OrderMapper.Mapper.Map<Order>(request);
            if (order == null) throw new ApplicationException("Order Command Not Mapped");
            return OrderMapper.Mapper.Map<OrderResponse>(await _repo.AddAsync(order));
        }
    }
}