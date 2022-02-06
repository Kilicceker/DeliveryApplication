using DeliveryApp.Core.Dtos;
using DeliveryApp.Web.Models;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Services
{
    public interface IAuthService
    {
        Task<User> GetAsync(string url,string token);
        Task<UserList> GetAllAsync(string url);
        Task AssignRoleAsync(UserRoleAssignDto userRoleAssignDto,string url);
        Task<User> RegisterAsync(UserRegisterDto userRegisterDto, string url);
        Task DeleteAsync(string url, string id);
        Task UpdateAsync(UserUpdateDto userUpdateDto, string url);
        Task ChangePasswordAsync(PasswordChangeDto passwordChangeDto, string url);
        Task<User> LoginAsync(UserLoginDto userLoginDto,string url);
        Task<User> GetCurrentUserAsync(string url, string token);
        Task<UserWithOrdersViewModel> GetUserWithOrdersAsync(string url);

    }
}
