using DeliveryApp.Core.Dtos;
using DeliveryApp.Shared.Result.Concrete;
using DeliveryApp.Web.Models;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Services
{
    public interface IBrandService
    {
        Task<Brand> GetAsync(string url);
        Task<SingleBrand> GetBrandAsync(string url);
        Task<BrandWithProducts> GetWithProductsAsync(string url);
        Task AddAsync(ProductBrandAddDto productBrandAddDto, string url);
        Task DeleteAsync(string url);
        Task UpdateAsync(ProductBrandUpdateDto productBrandUpdateDto, string url);
    }
}
