using DeliveryApp.Core.Dtos;
using DeliveryApp.Web.HttpService;
using DeliveryApp.Web.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Services
{
    public class AuthService : IAuthService
    {

        private readonly HttpClient _client;
        private readonly IApiService<User> _service;
        private readonly IApiService<UserList> _allusers;
        private readonly IApiService<UserWithOrdersViewModel> _userWithOrders;
        private readonly IApiService<UserUpdateDto> _update;
        private readonly IApiService<UserRoleAssignDto> _updateRole;
        private readonly IApiService<PasswordChangeDto> _change;
        private readonly IApiService<UserRegisterDto> _register;
        private readonly IApiService<UserLoginDto> _login;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IApiService<User> service, HttpClient client, IApiService<UserRegisterDto> register, IApiService<UserLoginDto> login, IHttpContextAccessor httpContextAccessor, IApiService<UserUpdateDto> update, IApiService<PasswordChangeDto> change, IApiService<UserWithOrdersViewModel> userWithOrders, IApiService<UserList> allusers, IApiService<UserRoleAssignDto> updateRole)
        {

            _service = service;
            _client = client;
            _register = register;
            _login = login;
            _httpContextAccessor = httpContextAccessor;
            var token = _httpContextAccessor.HttpContext.Request
        .Cookies["DeliveryApp"];
            if (!string.IsNullOrEmpty(token))
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            _update = update;
            _change = change;
            _userWithOrders = userWithOrders;
            _allusers = allusers;
            _updateRole = updateRole;
        }

        public async Task<User> RegisterAsync(UserRegisterDto userRegisterDto, string url)
        {
            return JsonSerializer.Deserialize<User>(await _register.AddAsync(userRegisterDto, url, _client), new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
        }

        public async Task DeleteAsync(string url, string id)
        {
            await _service.DeleteAsync(url + id, _client);
        }

        public async Task<User> GetAsync(string url,string token)
        {
            
            return await _service.GetAsync(url, _client);
        }
        public async Task<UserList> GetAllAsync(string url)
        {
            return await _allusers.GetAsync(url, _client);
        }
        public async Task<User> GetCurrentUserAsync(string url,string token)
        {
           // _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            return await _service.GetAsync(url, _client);
        }
        public async Task<User> LoginAsync(UserLoginDto userLoginDto, string url)
        {

           var response = await _login.AddAsync(userLoginDto, url, _client);
            return JsonSerializer.Deserialize<User>(response, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
        }

        public async Task UpdateAsync(UserUpdateDto userUpdateDto, string url)
        {
            await _update.UpdateAsync(userUpdateDto, url, _client);
        }

        public async Task ChangePasswordAsync(PasswordChangeDto passwordChangeDto, string url)
        {
            await _change.UpdateAsync(passwordChangeDto, url, _client);
        }

        public async Task<UserWithOrdersViewModel> GetUserWithOrdersAsync(string url)
        {
            var user = await _userWithOrders.GetAsync(url, _client);
            return user;
        }

        public async Task AssignRoleAsync(UserRoleAssignDto userRoleAssignDto, string url)
        {
            await _updateRole.UpdateAsync(userRoleAssignDto, url, _client);
        }
    }
}
