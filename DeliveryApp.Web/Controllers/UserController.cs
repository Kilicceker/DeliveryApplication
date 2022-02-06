using DeliveryApp.Core.Dtos;
using DeliveryApp.Web.Models;
using DeliveryApp.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IAddressService _address;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserController(IAuthService authService, IAddressService address, IHttpContextAccessor httpContextAccessor)
        {
            _authService = authService;
            _address = address;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public async Task<IActionResult> UserProfile(int userId)
        {
            var token = _httpContextAccessor.HttpContext.Request
.Cookies["DeliveryApp"];
            var user = await _authService.GetUserWithOrdersAsync($"https://localhost:44369/api/User/orders");
            var adress =await _address.GetUserAddressAsync($"https://localhost:44369/api/Address/user/{userId}");
            UserProfile userProfile = new UserProfile 
            { Id=user.Data.Id,
              Email= user.Data.Email,
              PhoneNumber= user.Data.PhoneNumber,
              UserName= user.Data.UserName,
              UserSurname= user.Data.UserSurname,
              Orders=user.Data.Orders,
              Address = adress.Data };
            return View(userProfile);
        }
        [HttpGet]
        public IActionResult PasswordChange()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeDto passwordChangeDto)
        {
            await _authService.ChangePasswordAsync(passwordChangeDto, "https://localhost:44369/api/User");
            return RedirectToAction("Logout", "Auth");
        }
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateModelView userUpdateModelView)
        {
            UserUpdateDto userUpdateDto = new UserUpdateDto
            {
                Id = userUpdateModelView.Id,
                Email=userUpdateModelView.Email,
                PhoneNumber=userUpdateModelView.PhoneNumber,
                UserName= userUpdateModelView.UserName,
                UserSurname = userUpdateModelView.UserSurname

            };
            await _authService.UpdateAsync(userUpdateDto, "https://localhost:44369/api/User");
            var adress = await _address.GetUserAddressAsync($"https://localhost:44369/api/Address/user/{userUpdateDto.Id}");
            if (adress.Data == null)
            {
                AddressAddDto addressAddDto = new AddressAddDto
                {
                    Street = userUpdateModelView.Street,
                    City = userUpdateModelView.City,
                    Neighbourhood = userUpdateModelView.Neighbourhood,
                    Defination = userUpdateModelView.Defination,
                    DoorNumber = userUpdateModelView.Defination
                };
                var result = await _address.AddAsync(addressAddDto, "https://localhost:44369/api/Address");
                return RedirectToAction("Logout", "Auth");
            }
            else
            {
                AddressUpdateDto addressUpdateDto = new AddressUpdateDto
                {
                    Id = userUpdateModelView.AddressId,
                    Street = userUpdateModelView.Street,
                    City = userUpdateModelView.City,
                    Neighbourhood = userUpdateModelView.Neighbourhood,
                    Defination = userUpdateModelView.Defination,
                    DoorNumber = userUpdateModelView.Defination
                };
                await _address.UpdateAsync(addressUpdateDto, "https://localhost:44369/api/Address");
            }
            return RedirectToAction("Logout", "Auth");
        }
    }
}
