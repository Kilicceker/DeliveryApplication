using DeliveryApp.Core.Dtos;
using DeliveryApp.Shared.Result.Concrete;
using DeliveryApp.Web.Models;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Services
{
    public interface IProductService
    {
        Task<ProductList> GetAllAsync(string url);
        Task<Product> GetAsync(string url);
        Task AddAsync(ProductAddDto productAddDto, string url);
        Task DeleteAsync(string url);
        Task UpdateAsync(ProductUpdateDto productUpdateDto, string url);
        Task UpdateRating(Rating rating, string url);
        Task<ProductListDto> SearchAsync(string url);
    }
}
