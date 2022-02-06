using Microsoft.AspNetCore.Mvc;
using DeliveryApp.Web.Models;
using System.Threading.Tasks;
using DeliveryApp.Web.Services;
using Microsoft.AspNetCore.Http;

namespace DeliveryApp.Web.ViewComponents
{
    public class Navbar : ViewComponent
    {
        private readonly IAuthService _auth;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Navbar(IAuthService auth, IHttpContextAccessor httpContextAccessor)
        {
            _auth = auth;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var token = _httpContextAccessor.HttpContext.Request
                    .Cookies["DeliveryApp"];
            if(!string.IsNullOrEmpty(token))
            {
                var auth = await _auth.GetCurrentUserAsync($"https://localhost:44369/api/User/current", token);
                return View(auth);
            }

            return View(new User { Data=null,ResultStatus=Shared.Result.ComplexTypes.ResultStatus.Error});
        }


    }
}
