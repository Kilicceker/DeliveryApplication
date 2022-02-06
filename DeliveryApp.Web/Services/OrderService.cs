using DeliveryApp.Core.Dtos;
using DeliveryApp.Web.Areas.Admin.Models;
using DeliveryApp.Web.HttpService;
using DeliveryApp.Web.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly IApiService<Order> _orders;
        private readonly IApiService<OrderUpdateDto> _update;
        private readonly IApiService<CreateOrderDto> _create;
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(IApiService<Order> orders, HttpClient client, IHttpContextAccessor httpContextAccessor, IApiService<OrderUpdateDto> update, IApiService<CreateOrderDto> create)
        {
            _orders = orders;
            _client = client;
            _httpContextAccessor = httpContextAccessor;
            var token = _httpContextAccessor.HttpContext.Request
.Cookies["DeliveryApp"];
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            _update = update;
            _create = create;
        }

        public async Task CreateOrdersAsync(CreateOrderDto createOrderDto,string url)
        {
            await _create.AddAsync(createOrderDto, url, _client);
        }

        public async Task<Order> GetOrdersAsync(string url)
        {
            return await _orders.GetAsync(url,_client);
        }

        public async Task UpdateOrderAsync(OrderUpdateDto orderUpdateDto, string url)
        {
            await _update.UpdateAsync(orderUpdateDto, url, _client);
        }
    }
}
