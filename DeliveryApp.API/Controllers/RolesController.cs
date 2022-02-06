using DeliveryApp.Core.Dtos;
using DeliveryApp.Core.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("Roles")]
        public async Task<IActionResult> CreateRoles()
        {
            await _roleService.CreateRoles();
            return NoContent();
        }
        [HttpGet()]
        public async Task<IActionResult> Roles()
        {
            var response=await _roleService.GetAllRolesAsync();
            return Ok(response);
        }
        [HttpPut()]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Roles(UserRoleAssignDto userRoleAssignDto)
        {
            var response = await _roleService.AssignRoleAsync(userRoleAssignDto);
            return Ok(response);
        }
    }
}
