using DeliveryApp.Core.Dtos;
using DeliveryApp.Shared.Result.Abstract;
using DeliveryApp.Shared.Result.Concrete;
using DeliveryApp.Web.Models;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Services
{
    public interface IAddressService
    {
        Task<Address> GetAllAsync(string url);
        Task<UserAddress> GetUserAddressAsync(string url);
        Task<Result> AddAsync(AddressAddDto addressAddDto, string url);
        Task UpdateAsync(AddressUpdateDto addressUpdateDto, string url);
    }
}
