using AutoMapper;
using DeliveryApp.Core.Dtos;
using DeliveryApp.Core.Entities.Concrete;

namespace DeliveryApp.Services.AutoMapper.Profiles
{
    public class ProducTypeProfile:Profile
    {
        public ProducTypeProfile()
        {
            CreateMap<ProductType, ProductTypeDto>().ReverseMap();

            CreateMap<ProductType, ProductTypeUpdateDto>().ReverseMap();
            CreateMap<ProductType, ProductTypeAddDto>().ReverseMap();
            CreateMap<ProductType, ProductTypeWithProductsDto>();
        }
    }
}
