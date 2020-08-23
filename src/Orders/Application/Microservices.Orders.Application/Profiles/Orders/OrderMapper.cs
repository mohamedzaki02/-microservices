using System;
using AutoMapper;

namespace Microservices.Orders.Application.Profiles.Orders
{
    public class OrderMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            cfg.AddProfile<OrderProfile>();
        });
        var mapper = config.CreateMapper();
        return mapper;
    });
        public static IMapper Mapper => Lazy.Value;
    }
}