using AutoMapper;
using DeliveryApp.Core.Dtos;
using DeliveryApp.Core.Entities.Concrete;
using DeliveryApp.Core.Services.Abstract;
using DeliveryApp.Shared.Result.Abstract;
using DeliveryApp.Shared.Result.ComplexTypes;
using DeliveryApp.Shared.Result.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IOrderService _orderService;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IRoleService _roleService;
        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, IConfiguration configuration, IOrderService orderService, IRoleService roleService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _configuration = configuration;
            _orderService = orderService;
            _roleService = roleService;
        }

        public async Task<IDataResult<UserDto>> GetUserAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var roles = await _userManager.GetRolesAsync(user);
            if (user != null)
            {
               var userToreturn= _mapper.Map<UserDto>(user);
               return new DataResult<UserDto>(ResultStatus.Succes, userToreturn);
            }
            return new DataResult<UserDto>(ResultStatus.Error, null);
        }
        public async Task<IDataResult<UserDto>> GetCurrentUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user);
            if (user != null)
            {
                var userToreturn = _mapper.Map<UserDto>(user);
                return new DataResult<UserDto>(ResultStatus.Succes, userToreturn);
            }
            return new DataResult<UserDto>(ResultStatus.Error, null);
        }

        public async Task<IDataResult<IList<UserListDto>>> GetUserListAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users == null)
                return new DataResult<IList<UserListDto>>(ResultStatus.Error, "No user found", null);
            var userList = _mapper.Map<IList<UserListDto>>(users);
            return new DataResult<IList<UserListDto>>(ResultStatus.Succes, userList);

        }

        public async Task<IResult> UserDeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var result = await _userManager.DeleteAsync(user);
            if(result.Succeeded)
            {
                return new Result(ResultStatus.Succes, $"{user.UserName} is deleted");
            }
            return new Result(ResultStatus.Error, $"An error occurred while deleting");
        }

        public async Task<IDataResult<UserDto>> UserLoginAsync(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDto.E_mail);
            if (user == null)
                return new DataResult<UserDto>(ResultStatus.Error, "User name or password incorrect",null);
            var result = await _signInManager.CheckPasswordSignInAsync(user,userLoginDto.Password,false);
            if(!result.Succeeded)
                return new DataResult<UserDto>(ResultStatus.Error, "User name or password incorrect",null);
            return new DataResult<UserDto>(ResultStatus.Succes, "Login successful",new UserDto 
            {
                Id=user.Id,
                UserName=user.UserName,
                UserSurname=user.UserSurname,
                Email=user.Email,
                PhoneNumber=user.PhoneNumber,
                Token=new TokenHandler(_configuration,_userManager).CreateToken(userLoginDto).Result
            });;;;;

        }

        public async Task<IResult> UserPaswordChangeAsync(PasswordChangeDto passwordChangeDto,ClaimsPrincipal user)
        {
            
            var usr = await _userManager.GetUserAsync(user);
            var isVerified = await _userManager.CheckPasswordAsync(usr, passwordChangeDto.CurrentPassword);
            if (isVerified)
            {
                var result = await _userManager.ChangePasswordAsync(usr, passwordChangeDto.CurrentPassword, passwordChangeDto.NewPassword);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(usr);
                    await _signInManager.SignOutAsync();
                    await _signInManager.PasswordSignInAsync(usr, passwordChangeDto.NewPassword, true, false);
                    return new Result(ResultStatus.Succes, "Password has been changed successfully");
                }
                return new Result(ResultStatus.Succes, "Check your current password");
            }
            return new Result(ResultStatus.Succes, "Check your information");
        }

        public async Task<IDataResult<UserDto>> UserRegisterAsync(UserRegisterDto userRegisterDto)
        {
            var user = _mapper.Map<User>(userRegisterDto);
            var result = await _userManager.CreateAsync(user,userRegisterDto.Password);
            if(!result.Succeeded)
            {
                return new DataResult<UserDto>(ResultStatus.Error,"Check your information",null);
            }
            var returnedUser = await _userManager.FindByEmailAsync(userRegisterDto.Email);
            List<string> roles = new List<string>();
            roles.Add("Member");
            UserRoleAssignDto userRoleAssignDto = new UserRoleAssignDto { UserId = returnedUser.Id.ToString(), Roles = roles };
            await _roleService.AssignRoleAsync(userRoleAssignDto);
            UserLoginDto userLoginDto = new UserLoginDto
            {
                E_mail = userRegisterDto.Email,
                Password = userRegisterDto.Password
            };
            return new DataResult<UserDto>(ResultStatus.Succes, "Resgister successful", new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                UserSurname = user.UserSurname,
                Email = user.Email,
                Token = new TokenHandler(_configuration, _userManager).CreateToken(userLoginDto).Result
            }); ; ; ; ;


        }

        public async Task<IResult> UserUpdateAsync(UserUpdateDto userUpdateDto)
        {
            var user = await _userManager.FindByIdAsync(userUpdateDto.Id.ToString());
            var updatedUser = _mapper.Map<UserUpdateDto, User>(userUpdateDto, user);
            var result = await _userManager.UpdateAsync(updatedUser);
            if (result.Succeeded)
                return new Result(ResultStatus.Succes, $"{userUpdateDto.UserName} has been updated");
            return new Result(ResultStatus.Error, $"An error occurred while updating");
        }

        public async Task<IDataResult<UserWithOrders>> GetUserWithOrdersAsync(string email)
        {
            var user =await _userManager.FindByEmailAsync(email);
            var orders = await _orderService.GetOrderAsync(user.Email);
            var userwithorders = _mapper.Map<UserWithOrders>(user);
            if(orders.Data.Count>5)
            {
                var returnOrders=orders.Data.OrderByDescending(x=>x.OrderDate).Skip(Math.Max(0, orders.Data.Count() - 5));
                userwithorders.Orders = _mapper.Map<IList<OrderListDto>>(returnOrders);
            }
            else
            {
                var returnOrders = orders.Data.OrderByDescending(x => x.OrderDate);
                userwithorders.Orders = _mapper.Map<IList<OrderListDto>>(returnOrders);
            }
            return new DataResult<UserWithOrders>(ResultStatus.Succes, userwithorders);
        }
    }
}
