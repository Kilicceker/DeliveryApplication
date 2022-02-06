using DeliveryApp.Core.Dtos;
using DeliveryApp.Core.Services.Abstract;
using DeliveryApp.Shared.Result.ComplexTypes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DeliveryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var response= await _userService.UserLoginAsync(userLoginDto);
            if (response.ResultStatus == ResultStatus.Error)
                return Unauthorized(response.Message);
            return Ok(response);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            var response = await _userService.UserRegisterAsync(userRegisterDto);
            if(response.ResultStatus==ResultStatus.Succes)
            {
                return Created(string.Empty, response);
            }
            return BadRequest(response);
        }
    }
}
