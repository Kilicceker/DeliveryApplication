using AutoMapper;
using DeliveryApp.Core.Dtos;
using DeliveryApp.Core.Entities.Concrete;

namespace DeliveryApp.Services.AutoMapper.Profiles
{
    public class ProductBrandProfile:Profile
    {
        public ProductBrandProfile()
        {
            CreateMap<ProductBrand, ProductBrandDto>().ReverseMap();

            CreateMap<ProductBrand, ProductBrandUpdateDto>().ReverseMap();
            CreateMap<ProductBrand, ProductBrandAddDto>().ReverseMap();
            CreateMap<ProductBrand, ProductBrandWithProductsDto>();
        }
    }
}
