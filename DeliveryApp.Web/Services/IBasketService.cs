using DeliveryApp.Core.Entities.Concrete;
using DeliveryApp.Web.Models;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Services
{
    public interface IBasketService
    {
        Task<CustomerBasket> GetAsync(string url);
        Task<string> AddAsync(Basket basket, string url);
        Task DeleteAsync(string url);
        Task UpdateAsync(CustomerBasket basket, string url);

    }
}
