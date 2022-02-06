using DeliveryApp.Core.Dtos;
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
    public class BrandController : Controller
    {
        private readonly IBrandService _brand;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BrandController(IBrandService brand, IHttpContextAccessor httpContextAccessor)
        {
            _brand = brand;
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
            var model = await _brand.GetAsync("https://localhost:44369/api/Brands");
            return View(model);
        }
        [HttpGet]
        public IActionResult AddBrand()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBrand(ProductBrandAddDto productBrandAddDto)
        {
            if (ModelState.IsValid)
            {
                await _brand.AddAsync(productBrandAddDto, "https://localhost:44369/api/Brands");
                return RedirectToAction("Index", "Brand");
            }
            return View(productBrandAddDto);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateBrand(int brandId)
        {
            var brand = await _brand.GetBrandAsync($"https://localhost:44369/api/Brands/{brandId}");
            ProductBrandUpdateDto productBrandUpdateDto = new ProductBrandUpdateDto { Id = brand.Data.Id, Name = brand.Data.Name };
            return View(productBrandUpdateDto);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBrand(ProductBrandUpdateDto productBrandUpdateDto)
        {
            if (ModelState.IsValid)
            {
                await _brand.UpdateAsync(productBrandUpdateDto, "https://localhost:44369/api/Brands");
                return RedirectToAction("Index", "Brand");
            }
            return View(productBrandUpdateDto);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteBrand(int brandId)
        {
            await _brand.DeleteAsync($"https://localhost:44369/api/Brands/{brandId}");
            return RedirectToAction("Index", "Brand");
        }
    }
}
