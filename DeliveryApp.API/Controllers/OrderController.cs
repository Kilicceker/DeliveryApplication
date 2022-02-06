using DeliveryApp.Core.Dtos;
using DeliveryApp.Core.Entities.Concrete;
using DeliveryApp.Core.Services.Abstract;
using DeliveryApp.Shared.Result.ComplexTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DeliveryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;
        public OrderController(IOrderService orderService, IHttpContextAccessor contextAccessor, UserManager<User> userManager)
        {
            _orderService = orderService;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto createOrderDto)
        {
            var userEmail = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var order = await _orderService.CreateOrderAsync(createOrderDto.Id, userEmail);
            if (order.ResultStatus == ResultStatus.Error)
                return BadRequest(order);
            return Ok(order);
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Courier")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetOrdersAsync();
            if (orders.ResultStatus == ResultStatus.Error)
                return BadRequest(orders);
            return Ok(orders);
        }
        [HttpGet("user")]
        public async Task<IActionResult> GetUserOrders()
        {
            var userEmail = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var orders = await _orderService.GetOrderAsync(userEmail);
            if (orders.ResultStatus == ResultStatus.Error)
                return BadRequest(orders);
            return Ok(orders);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> OrderCancel(int id)
        {
            var orderCancel = await _orderService.OrderCancelAsync(id);
            if (orderCancel.ResultStatus == ResultStatus.Info)
                return BadRequest(orderCancel);
            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(OrderUpdateDto orderUpdateDto)
        {
            var order = await _orderService.UpdateOrderAsync(orderUpdateDto);
            if (order.ResultStatus == ResultStatus.Error)
                return BadRequest(order);
            return NoContent();
        }
    }
}
