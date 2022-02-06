using DeliveryApp.Core.Entities.Concrete;
using DeliveryApp.Web.HttpService;
using DeliveryApp.Web.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _client;
        private readonly IApiService<Basket> _service;
        private readonly IApiService<CustomerBasket> _update;
        private readonly IApiService<CustomerBasket> _basket;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketService(IApiService<Basket> service, HttpClient client, IHttpContextAccessor httpContextAccessor, IApiService<CustomerBasket> update, IApiService<CustomerBasket> basket)
        {
            _client = client;
            _service = service;
            _httpContextAccessor = httpContextAccessor;
            var token = _httpContextAccessor.HttpContext.Request
                    .Cookies["DeliveryApp"];
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            _update = update;
            _basket = basket;
        }

        public async Task<string> AddAsync(Basket basket, string url)
        {
            return await _service.AddAsync(basket, url, _client);
        }

        public async Task DeleteAsync(string url)
        {
            await _service.DeleteAsync(url, _client);
        }

        public async Task<CustomerBasket> GetAsync(string url)
        {
            return await _basket.GetAsync(url, _client);
        }

        public async Task UpdateAsync(CustomerBasket basket, string url)
        {
            await _update.UpdateAsync(basket, url, _client);
        }
    }
}
