using AutoMapper;
using EventBusRabbitMQ.Events;
using Microservices.Orders.Application.Commands;

namespace Microservices.Orders.API.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<BasketCheckoutEvent, BasketCheckoutCommand>();
        }
    }
}