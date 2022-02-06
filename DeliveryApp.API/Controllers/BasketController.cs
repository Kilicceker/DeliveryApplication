using DeliveryApp.Core.Entities.Concrete;
using DeliveryApp.Core.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DeliveryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basket;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;
        public BasketController(IBasketRepository basket, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, IHttpContextAccessor contextAccessor)
        {
            _basket = basket;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            
            var userEmail = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var userId = (await _userManager.FindByEmailAsync(userEmail)).Id;
            var basket = await _basket.GetBasketAsync(userId.ToString());
            return Ok(basket ?? new CustomerBasket(userId.ToString()));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateBasket(CustomerBasket basket)
        {
            var userEmail = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var userId = (await _userManager.FindByEmailAsync(userEmail)).Id;
            basket.Id = userId.ToString();
            var updatedBasket = await _basket.UpdateBasketAsync(basket);
            return Ok(updatedBasket);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBasketAsync(string id)
        {
            await _basket.DeleteBasketAsync(id);
            return NoContent();
        }
        [HttpDelete("product")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var userEmail = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var userId = (await _userManager.FindByEmailAsync(userEmail)).Id;
            var deleted = await _basket.DeleteProductFromBasketAsync(userId.ToString(), id);
            if(deleted==true)
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}
