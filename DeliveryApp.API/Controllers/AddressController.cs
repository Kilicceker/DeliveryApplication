using DeliveryApp.Core.Dtos;
using DeliveryApp.Core.Services.Abstract;
using DeliveryApp.Shared.Result.ComplexTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DeliveryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly IHttpContextAccessor _contextAccessor;

        public AddressController(IAddressService addressService, IHttpContextAccessor contextAccessor)
        {
            _addressService = addressService;
            _contextAccessor = contextAccessor;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var address = await _addressService.GetAsync(id);
            if (address.ResultStatus == ResultStatus.Error)
                return BadRequest(address);
            return Ok(address);
        }
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserAdress(int id)
        {
            var address = await _addressService.GetWithUserIdAsync(id);
            if (address.ResultStatus == ResultStatus.Error)
                return BadRequest(address);
            return Ok(address);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var address = await _addressService.GetAllAsync();
            if (address.ResultStatus == ResultStatus.Error)
                return BadRequest(address);
            return Ok(address);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddressAddDto addressDto)
        {
            var userEmail = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            return Created(string.Empty,await _addressService.AddAsync(addressDto, userEmail));
        }
        [HttpPut]
        public async Task<IActionResult> Update(AddressUpdateDto addressDto)
        {
            var userEmail = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var result = await _addressService.UpdateAsync(addressDto,userEmail);
            if (result.ResultStatus == ResultStatus.Error)
                return BadRequest(result);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _addressService.DeleteAsync(id);
            if (result.ResultStatus == ResultStatus.Error)
                return BadRequest(result);
            return NoContent();
        }
    }
}
