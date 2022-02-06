
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
    public class ProductService:IProductService
    {
        private readonly HttpClient _client;
        private readonly IApiService<ProductList> _service;
        private readonly IApiService<ProductAddDto> _add;
        private readonly IApiService<Search> _search;
        private readonly IApiService<Product> _singleProduct;
        private readonly IApiService<ProductUpdateDto> _updateService;
        private readonly IApiService<Rating> _updateRating;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductService(HttpClient client, IApiService<ProductList> service, IApiService<ProductUpdateDto> updateService, IApiService<Product> singleProduct, IHttpContextAccessor httpContextAccessor, IApiService<Rating> updateRating, IApiService<Search> search, IApiService<ProductAddDto> add)
        {
            _client = client;
            _service = service;
            _updateService = updateService;
            _singleProduct = singleProduct;
            _httpContextAccessor = httpContextAccessor;
            var token = _httpContextAccessor.HttpContext.Request
.Cookies["DeliveryApp"];
            if (!string.IsNullOrEmpty(token))
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            _updateRating = updateRating;
            _search = search;
            _add = add;
        }

        public async Task AddAsync(ProductAddDto productAddDto,string url)
        {
            await _add.AddAsync(productAddDto, url, _client);
        }

        public async Task DeleteAsync(string url)
        {
            await _service.DeleteAsync(url, _client);
        }

        public async Task<ProductList> GetAllAsync(string url)
        {
            return await _service.GetAsync(url, _client);
        }
        public async Task<Product> GetAsync(string url)
        {
            return await _singleProduct.GetAsync(url, _client);
        }
        public async Task<ProductListDto> SearchAsync(string url)
        {
            return (await _search.GetAsync(url, _client)).Data;
        }

        public async Task UpdateAsync(ProductUpdateDto productUpdateDto, string url)
        {
            await _updateService.UpdateAsync(productUpdateDto, url, _client);
        }

        public async Task UpdateRating(Rating rating,string url)
        {
            await _updateRating.UpdateAsync(rating, url, _client);
        }
    }
}
