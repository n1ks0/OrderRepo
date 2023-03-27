using AutoMapper;
using Order.BL.Entities;
using Order.Web.Models.Order;
using System.Linq;

namespace Order.Web.MapperProfile
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderItemCreateDto, OrderItem>();
            CreateMap<OrderItemEditDto, OrderItem>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();

            CreateMap<OrderCreateViewModel, Order.BL.Entities.Order>()
                .ForMember(dst => dst.OrderItems, x => x.MapFrom(o => o.OrderItems));           

            CreateMap<BL.Entities.Order, OrderIndexDto>()
                .ForMember(dst => dst.Quantity, src => src.MapFrom(o => o.OrderItems.Sum(oi => oi.Quantity)))
                .ForMember(dst => dst.Provider, src => src.MapFrom(o => o.Provider.Name));

            CreateMap<BL.Entities.Order, OrderEditViewModel>()
                .ForMember(dst => dst.OrderItems, src => src.MapFrom(o => o.OrderItems));

            CreateMap<OrderEditViewModel, BL.Entities.Order>()
                .ForMember(dst => dst.OrderItems, src => src.MapFrom(o => o.OrderItems));

            CreateMap<BL.Entities.Order, OrderInfoViewModel>()
                .ForMember(dst => dst.Provider, src => src.MapFrom(o => o.Provider.Name))
                .ForMember(dst => dst.OrderItems, src => src.MapFrom(o => o.OrderItems));
        }
    }
}
