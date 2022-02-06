using DeliveryApp.Core.Dtos;
using DeliveryApp.Core.Services.Abstract;
using DeliveryApp.Shared.Result.ComplexTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DeliveryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserController(IUserService userService, IHttpContextAccessor contextAccessor)
        {
            _userService = userService;
            _contextAccessor = contextAccessor;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> User(int id)
        {
            var user = await _userService.GetUserAsync(id);
            return Ok(user);
        }
        [HttpGet("current")]
        public async Task<IActionResult> CurrentUser()
        {
            var userEmail = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var user = await _userService.GetCurrentUserAsync(userEmail);
            return Ok(user);
        }
        [HttpGet]
        public async Task<IActionResult> User()
        {
            var user = await _userService.GetUserListAsync();
            if (user.ResultStatus == ResultStatus.Succes)
                return Ok(user);
            return BadRequest(user);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove(int id)
        {
            var response = await _userService.UserDeleteAsync(id);
            if (response.ResultStatus == ResultStatus.Succes)
                return NoContent();
            return NotFound(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            var response = await _userService.UserUpdateAsync(userUpdateDto);
            if (response.ResultStatus == ResultStatus.Succes)
                return NoContent();
            return BadRequest(response);
        }
        [HttpPost]
        public async Task<IActionResult> Update(PasswordChangeDto passwordChangeDto)
        {
            var response = await _userService.UserPaswordChangeAsync(passwordChangeDto, HttpContext.User);
            if (response.ResultStatus==ResultStatus.Succes)
            {
                return NoContent();
            }
            return BadRequest(response);
        }
        [HttpGet("orders")]
        public async Task<IActionResult> UserWithOrders()
        {
            var userEmail = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var user = await _userService.GetUserWithOrdersAsync(userEmail);
            return Ok(user);
        }
    }
}
