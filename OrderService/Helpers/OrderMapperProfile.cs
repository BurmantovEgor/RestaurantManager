using AutoMapper;
using OrderService.Core;
using OrderService.Data.Entities;

namespace OrderService.Helpers
{
    public class OrderMapperProfile:Profile
    {
        public OrderMapperProfile()
        {
             CreateMap<CreateOrderDTO, OrderEntity>()
     .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
     .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
     .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails))
     .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice));

             CreateMap<CreateOrderDetailsDTO, OrderDetailEntity>()
     .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
     .ForMember(dest => dest.OrderId, opt => opt.Ignore())
     .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
     .ForMember(dest => dest.ProductCount, opt => opt.MapFrom(src => src.ProductCount));

        }
    }
}
