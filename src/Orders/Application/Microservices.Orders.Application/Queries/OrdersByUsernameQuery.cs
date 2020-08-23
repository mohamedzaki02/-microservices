using System;
using System.Collections.Generic;
using MediatR;
using Microservices.Orders.Application.Responses;

namespace Microservices.Orders.Application.Queries
{
    public class OrdersByUsernameQuery : IRequest<IEnumerable<OrderResponse>>
    {
        public string Username { get; set; }
        public OrdersByUsernameQuery(string username)
        {
            this.Username = username ?? throw new ArgumentNullException(nameof(username));
        }
    }
}