using AutoMapper;
using OnlineShopWebAPI.DataTransferObject;
using OnlineShopWebAPI.Model;

namespace OnlineShopWebAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
