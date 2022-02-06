using DeliveryApp.Core.Dtos;
using DeliveryApp.Shared.Result.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Core.Services.Abstract
{
    public interface IRoleService
    {
        Task<IResult> AssignRoleAsync(UserRoleAssignDto userRoleAssignDto);
        Task<IDataResult<IList<RoleDto>>> GetAllRolesAsync();
        Task<IResult> RemoveRoleAsync(string userId, UserRoleAssignDto userRoleAssignDto);
        Task CreateRoles();
    }
}
