using DeliveryApp.Core.Dtos;
using DeliveryApp.Shared.Result.Concrete;
using DeliveryApp.Web.HttpService;
using DeliveryApp.Web.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _client;
        private readonly IApiService<Category> _service;
        private readonly IApiService<SingleCategory> _single;
        private readonly IApiService<ProductTypeAddDto> _add;
        private readonly IApiService<ProductTypeUpdateDto> _updateService;
        private readonly IApiService<CategoryWithProducts> _categories;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CategoryService(HttpClient client, IApiService<Category> service, IApiService<ProductTypeUpdateDto> updateService, IApiService<CategoryWithProducts> categories, IHttpContextAccessor httpContextAccessor, IApiService<ProductTypeAddDto> add, IApiService<SingleCategory> single)
        {

            _client = client;
            _service = service;
            _updateService = updateService;
            _categories = categories;
            _httpContextAccessor = httpContextAccessor;
            var token = _httpContextAccessor.HttpContext.Request
.Cookies["DeliveryApp"];
            if (!string.IsNullOrEmpty(token))
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            _add = add;
            _single = single;
        }

        public async Task AddAsync(ProductTypeAddDto productTypeAddDto, string url)
        {
            await _add.AddAsync(productTypeAddDto, url, _client);
        }

        public async Task DeleteAsync(string url)
        {
            await _service.DeleteAsync(url, _client);
        }

        public async Task<Category> GetAsync(string url)
        {
            return await _service.GetAsync(url, _client);
        }

        public async Task<SingleCategory> GetCategorAsync(string url)
        {
            return await _single.GetAsync(url,_client);
        }

        public Task<CategoryWithProducts> GetWithProductsAsync(string url)
        {
           return _categories.GetAsync(url, _client);
            
        }

        public async Task UpdateAsync(ProductTypeUpdateDto productTypeUpdateDto, string url)
        {
            await _updateService.UpdateAsync(productTypeUpdateDto, url, _client);
        }
    }
}
