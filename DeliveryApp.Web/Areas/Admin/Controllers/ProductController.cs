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
    public class ProductController : Controller
    {
        private readonly IProductService _product;
        private readonly ICategoryService _category;
        private readonly IBrandService _brand;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductController(IProductService product, ICategoryService category, IBrandService brand, IHttpContextAccessor httpContextAccessor)
        {
            _product = product;
            _category = category;
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
            var model = await _product.GetAllAsync("https://localhost:44369/api/Products/All");
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            var categories = (await _category.GetAsync("https://localhost:44369/api/Types")).Data;
            var brands = (await _brand.GetAsync("https://localhost:44369/api/Brands")).Data;
            ProductAddViewModel productAddViewModel = new ProductAddViewModel {Categories=categories,Brands=brands };
            return View(productAddViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductAddDto productAddDto)
        {
            if(ModelState.IsValid)
            {
                 await _product.AddAsync(productAddDto, "https://localhost:44369/api/Products");
                
                return RedirectToAction("Index", "Product");
            }
            return RedirectToAction("Index","Error");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int productId)
        {
            var categories = (await _category.GetAsync("https://localhost:44369/api/Types")).Data;
            var brands = (await _brand.GetAsync("https://localhost:44369/api/Brands")).Data;
            var product = (await _product.GetAsync($"https://localhost:44369/api/Products/{productId}")).Data;
            ProductUpdateViewModel productUpdate = new ProductUpdateViewModel { Categories = categories, Brands = brands,Product=product};
            return View(productUpdate);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductUpdateDto productUpdateDto)
        {
            if (ModelState.IsValid)
            {
                await _product.UpdateAsync(productUpdateDto, "https://localhost:44369/api/Products");
                return RedirectToAction("Index", "Product");
            }
            var categories = (await _category.GetAsync("https://localhost:44369/api/Types")).Data;
            var brands = (await _brand.GetAsync("https://localhost:44369/api/Brands")).Data;
            var product = (await _product.GetAsync($"https://localhost:44369/api/Products/{productUpdateDto.Id}")).Data;
            ProductUpdateViewModel productUpdate = new ProductUpdateViewModel { Categories = categories, Brands = brands, Product = product };
            return View(productUpdate);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            await _product.DeleteAsync($"https://localhost:44369/api/Products/{productId}");
            return RedirectToAction("Index", "Product");
        }
    }
}
