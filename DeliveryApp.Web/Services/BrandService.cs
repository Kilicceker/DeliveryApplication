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
    public class BrandService : IBrandService
    {
        private readonly HttpClient _client;
        private readonly IApiService<Brand> _service;
        private readonly IApiService<SingleBrand> _single;
        private readonly IApiService<ProductBrandAddDto> _add;
        private readonly IApiService<ProductBrandUpdateDto> _update;
        private readonly IApiService<BrandWithProducts> _brands;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BrandService(IApiService<Brand> service, HttpClient client, IApiService<ProductBrandUpdateDto> update, IApiService<BrandWithProducts> brands, IHttpContextAccessor httpContextAccessor, IApiService<ProductBrandAddDto> add, IApiService<SingleBrand> single)
        {
            _service = service;
            _client = client;
            _update = update;
            _brands = brands;
            _httpContextAccessor = httpContextAccessor;
            var token = _httpContextAccessor.HttpContext.Request
.Cookies["DeliveryApp"];
            if (!string.IsNullOrEmpty(token))
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            _add = add;
            _single = single;
        }

        public async Task AddAsync(ProductBrandAddDto productBrandAddDto, string url)
        {
            await _add.AddAsync(productBrandAddDto, url, _client);
        }

        public async Task DeleteAsync(string url)
        {
            await _service.DeleteAsync(url, _client);
        }

        public async Task<Brand> GetAsync(string url)
        {
            return await _service.GetAsync(url, _client);
        }

        public async Task<SingleBrand> GetBrandAsync(string url)
        {
            return await _single.GetAsync(url, _client);
        }

        public async Task<BrandWithProducts> GetWithProductsAsync(string url)
        {
            return await _brands.GetAsync(url, _client);
        }

        public async Task UpdateAsync(ProductBrandUpdateDto productBrandUpdateDto, string url)
        {
            await _update.UpdateAsync(productBrandUpdateDto, url, _client);
        }

        
    }
}
