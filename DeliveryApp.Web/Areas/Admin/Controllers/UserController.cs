using DeliveryApp.Core.Dtos;
using DeliveryApp.Web.Areas.Admin.Models;
using DeliveryApp.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IAuthService authService, IHttpContextAccessor httpContextAccessor)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = _httpContextAccessor.HttpContext.Request
.Cookies["DeliveryApp"];
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var roles = jwtSecurityToken.Claims.First(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            if (!roles.Value.Contains("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            var users = await _authService.GetAllAsync("https://localhost:44369/api/User");
            return View(users);
        }
        [HttpPost]
        public async Task<IActionResult> RoleUpdate(UserUpdateViewModel userUpdateViewModel)
        {
            List<string> roles = new List<string>();
            roles.Add(userUpdateViewModel.Role);
            await _authService.AssignRoleAsync(new UserRoleAssignDto{UserId=userUpdateViewModel.Id.ToString(),
            Roles=roles
            }, "https://localhost:44369/api/Roles");
            return RedirectToAction("Index", "User");
        }
    }
}
