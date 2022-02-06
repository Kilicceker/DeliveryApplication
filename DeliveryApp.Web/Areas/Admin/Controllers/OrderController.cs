using DeliveryApp.Core.Dtos;
using DeliveryApp.Core.Entities.Concrete;
using DeliveryApp.Web.Areas.Admin.Models;
using DeliveryApp.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Areas.Admin.Controllers
{  
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetOrdersAsync("https://localhost:44369/api/Order");
            return View(orders);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateOrder(OrderUpdate orderUpdate )
        {
            OrderUpdateDto orderUpdateDto = new OrderUpdateDto();
            if (orderUpdate.OrderStatus == 1) { orderUpdateDto.Status = OrderStatus.InDelivery; }
            if (orderUpdate.OrderStatus == 2) { orderUpdateDto.Status = OrderStatus.Delivered; }
            orderUpdateDto.Id = orderUpdate.Id;

            orderUpdateDto.IsCanceled = false;
            await _orderService.UpdateOrderAsync(orderUpdateDto,"https://localhost:44369/api/Order");
            return RedirectToAction("Index","Home");
        }
    }
}
