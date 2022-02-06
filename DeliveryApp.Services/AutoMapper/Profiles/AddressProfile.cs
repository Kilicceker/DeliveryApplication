using AutoMapper;
using DeliveryApp.Core.Dtos;
using DeliveryApp.Core.Entities.Concrete;

namespace DeliveryApp.Services.AutoMapper.Profiles
{
    public class AddressProfile:Profile
    {
        public AddressProfile()
        {
            CreateMap<AddressDto, Adress>().ReverseMap();
            CreateMap<AddressAddDto, Adress>().ReverseMap();
            CreateMap<AddressUpdateDto, Adress>().ReverseMap();
        }
    }
}
