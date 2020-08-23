using AutoMapper;
using Microservices.Orders.Application.Commands;
using Microservices.Orders.Application.Responses;
using Microservices.Orders.Core.Entities;

namespace Microservices.Orders.Application.Profiles.Orders
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<BasketCheckoutCommand, Order>();
        }
    }
}