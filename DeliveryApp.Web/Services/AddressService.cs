using DeliveryApp.Core.Dtos;
using DeliveryApp.Shared.Result.Abstract;
using DeliveryApp.Shared.Result.Concrete;
using DeliveryApp.Web.HttpService;
using DeliveryApp.Web.Models;
using DeliveryApp.Web.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Services
{
    public class AddressService : IAddressService
    {
        private readonly IApiService<UserAddress> _singleAddress;
        private readonly IApiService<AddressUpdateDto> _update;
        private readonly IApiService<AddressAddDto> _add;
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AddressService(IApiService<UserAddress> singleAddress, HttpClient client, IHttpContextAccessor httpContextAccessor, IApiService<AddressUpdateDto> update, IApiService<AddressAddDto> add)
        {
            _singleAddress = singleAddress;
            _client = client;
            _httpContextAccessor = httpContextAccessor;
            var token = _httpContextAccessor.HttpContext.Request
.Cookies["DeliveryApp"];
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            _update = update;
            _add = add;
        }

        public async Task<Result> AddAsync(AddressAddDto addressAddDto, string url)
        {
            var response = await _add.AddAsync(addressAddDto, url, _client);
            var result= JsonSerializer.Deserialize<Result>(response, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return result;
        }

        public Task<Address> GetAllAsync(string url)
        {
            throw new NotImplementedException();
        }

        public async Task<UserAddress> GetUserAddressAsync(string url)
        {
            var response = await _singleAddress.GetAsync(url,_client);
            return response;
        }

        public async Task UpdateAsync(AddressUpdateDto addressUpdateDto, string url)
        {
            await _update.UpdateAsync(addressUpdateDto, url,_client);
        }
    }
}
