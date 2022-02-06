using DeliveryApp.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Controllers
{    
    public class HomeController : Controller
    {
        private readonly IProductService _product;
        public HomeController(IProductService product)
        {
            _product = product;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _product.GetAllAsync("https://localhost:44369/api/Products?currentPage=1&pageSize=10&isAscending=false");
            return View(model);
        }
    }
}
