using DeliveryApp.Core.Dtos;
using DeliveryApp.Shared.Result.Concrete;
using DeliveryApp.Web.Models;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Services
{
    public interface ICategoryService
    {
        Task<Category> GetAsync(string url);
        Task<SingleCategory> GetCategorAsync(string url);
        Task<CategoryWithProducts> GetWithProductsAsync(string url);
        Task AddAsync(ProductTypeAddDto productTypeAddDto, string url);
        Task DeleteAsync(string url);
        Task UpdateAsync(ProductTypeUpdateDto productTypeUpdateDto, string url);
    }
}
