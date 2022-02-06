using AutoMapper;
using DeliveryApp.Core.Dtos;
using DeliveryApp.Core.Entities.Concrete;
using DeliveryApp.Core.Services.Abstract;
using DeliveryApp.Shared.Result.Abstract;
using DeliveryApp.Shared.Result.ComplexTypes;
using DeliveryApp.Shared.Result.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Concrete
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;

        public RoleService(UserManager<User> userManager, IMapper mapper, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<IResult> AssignRoleAsync(UserRoleAssignDto userRoleAssignDto)
        {
            var user = await _userManager.FindByIdAsync(userRoleAssignDto.UserId);
            foreach (var role in userRoleAssignDto.Roles)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            await _userManager.UpdateSecurityStampAsync(user);
            return new Result(ResultStatus.Succes, "roles successfully assigned");
        }

        public async Task<IResult> RemoveRoleAsync(string userId, UserRoleAssignDto userRoleAssignDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            foreach (var role in userRoleAssignDto.Roles)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }
            await _userManager.UpdateSecurityStampAsync(user);
            return new Result(ResultStatus.Succes, "roles successfully removed");
        }

        public async Task CreateRoles()
        {
            string[] roleNames = { "Admin", "Courier", "Member" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1
                    var role = new Role();
                    role.Name = roleName;
                   await _roleManager.CreateAsync(role);
                }
            }
        }

        public async Task<IDataResult<IList<RoleDto>>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var roleList = _mapper.Map<IList<RoleDto>>(roles);
            return new DataResult<IList<RoleDto>>(ResultStatus.Succes, roleList); 
        }
    }
}
