using DeliveryApp.Core.Dtos;
using DeliveryApp.Shared.Result.ComplexTypes;
using DeliveryApp.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Controllers
{
    public class AuthController : Controller
    {

        private readonly IAuthService _auth;
        public AuthController(IAuthService auth)
        {

            _auth = auth;

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
         public async Task<IActionResult> Login([FromForm] UserLoginDto userLoginDto)
         {
            if (ModelState.IsValid)
            {
                var response = await _auth.LoginAsync(userLoginDto, "https://localhost:44369/api/Auth/Login");
                if (response.ResultStatus == ResultStatus.Error)
                    return View(userLoginDto);
                var token = response.Data.Token;
                Response.Cookies.Append("DeliveryApp", token, new Microsoft.AspNetCore.Http.CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.Now.AddHours(1)
                }); ;
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
                return RedirectToAction("Auth", "Login");
        }
        [HttpPost] 
         public async Task<IActionResult> Register([FromForm] UserRegisterDto userRegisterDto)
         {
            if (ModelState.IsValid)
            {
                var response = await _auth.RegisterAsync(userRegisterDto, "https://localhost:44369/api/Auth/Register");
                if (response.ResultStatus == ResultStatus.Succes)
                {
                    var token = response.Data.Token;
                    Response.Cookies.Append("DeliveryApp", token, new Microsoft.AspNetCore.Http.CookieOptions
                    {
                        HttpOnly = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTimeOffset.Now.AddHours(1)
                    });
                    return RedirectToAction("Index", "Home");
                }
                return View(userRegisterDto);
            }
            return View("Login",userRegisterDto);
        }
        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("DeliveryApp");
            return RedirectToAction("Index", "Home");
        }
    }

}
