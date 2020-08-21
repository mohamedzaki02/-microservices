using AutoMapper;
using EventBusRabbitMQ.Events;
using Microservices.Basket.API.Entities;

namespace Microservices.Basket.API.Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>();
        }
    }
}