using DeliveryApp.Core.Dtos;
using DeliveryApp.Core.Entities.Concrete;
using DeliveryApp.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        public BasketController(IBasketService basketService, IProductService productService, IOrderService orderService)
        {
            _basketService = basketService;
            _productService = productService;
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var basket = await _basketService.GetAsync("https://localhost:44369/api/Basket");
            return View(basket);
        }
        [HttpGet]
        public async Task<IActionResult> AddSingle(int productId)
        {
            var basket = await _basketService.GetAsync("https://localhost:44369/api/Basket");
            var product = await _productService.GetAsync($"https://localhost:44369/api/Products/{productId}");
            basket.Items.Add(new BasketItem
            {
                Id = product.Data.Id,
                Name = product.Data.Name,
                Price = product.Data.Price,
                Quantity=1,
                PictureUrl=product.Data.PictureUrl,
                ProductBrand=product.Data.ProductBrand,
                ProductType=product.Data.ProductType
            }) ;
            await _basketService.UpdateAsync(basket, "https://localhost:44369/api/Basket");
            return RedirectToAction("Index","Basket");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int productId)
        {
            await _basketService.DeleteAsync($"https://localhost:44369/api/Basket/product?id={productId}");
            return RedirectToAction("Index", "Basket");
        }
        [HttpPost]
        public async Task<IActionResult> Add(BasketItem item)
        {
            var basket = await _basketService.GetAsync("https://localhost:44369/api/Basket");
            basket.Items.Add(item);
            await _basketService.UpdateAsync(basket, "https://localhost:44369/api/Basket");
            return RedirectToAction("Index","Basket");
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto createOrderDto)
        {
            await _orderService.CreateOrdersAsync(createOrderDto, "https://localhost:44369/api/Order");
            return RedirectToAction("UserProfile", "User");
        }

    }
}
