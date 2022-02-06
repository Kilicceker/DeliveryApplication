using DeliveryApp.Core.Dtos;
using DeliveryApp.Web.Models;
using DeliveryApp.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _product;
        private readonly ICategoryService _category;
        private readonly IBrandService _brand;
        public ProductController(IProductService product, ICategoryService category, IBrandService brand)
        {

            _product = product;
            _category = category;
            _brand = brand;
        }
        [HttpGet]
        public async Task<IActionResult> AllProducts()
        {
            var categories = await _category.GetAsync("https://localhost:44369/api/Types");
            AllProductsViewModel allProductsViewModel = new AllProductsViewModel();
            foreach (var category in categories.Data)
            {
                var products = (await _category.GetWithProductsAsync($"https://localhost:44369/api/Types/{category.Id}/products")).Data;
                ProductTypeWithProductsDto productTypeWithProductsDto = new ProductTypeWithProductsDto
                {
                    Id = category.Id,
                    Name=category.Name,
                    Products=products.Products
                };
                allProductsViewModel.Products.Add(productTypeWithProductsDto);
            }
            return View(allProductsViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _product.GetAsync("https://localhost:44369/api/Products/");
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> CategoryWithProducts(int categoryId)
        {
            var model = await _category.GetWithProductsAsync($"https://localhost:44369/api/Types/{categoryId}/products");
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> BrandWithProducts(int brandId)
        {
            var model = await _brand.GetWithProductsAsync($"https://localhost:44369/api/Brands/{brandId}/products");
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int productId)
        {
            var model = await _product.GetAsync($"https://localhost:44369/api/Products/{productId}");
            var recomended = await _product.GetAllAsync($"https://localhost:44369/api/Products?productTypeId={model.Data.ProductTypeId}&currentPage=1&pageSize=3&isAscending=false");
            ProductWithRecomendedModelView modelview = new ProductWithRecomendedModelView
            {
                Product = model.Data,
                RecomendedProducts = recomended.Data
            };
            return View(modelview);
        }
        [HttpGet]
        public async Task<IActionResult> Search(string keyword)
        {
            var model = await _product.SearchAsync($"https://localhost:44369/api/Products/Search?keyword={keyword}&currentPage=1&pageSize=1000&isAscending=false");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRating(Rating rating)
        {
            await _product.UpdateRating(rating, "https://localhost:44369/api/Products/rating");
            return RedirectToAction("Index", "Home");
        }
    }
}
