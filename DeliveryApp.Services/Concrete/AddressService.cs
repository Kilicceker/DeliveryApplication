using AutoMapper;
using DeliveryApp.Core.Dtos;
using DeliveryApp.Core.Entities.Concrete;
using DeliveryApp.Core.Services.Abstract;
using DeliveryApp.Core.UnitOfWorks;
using DeliveryApp.Shared.Result.Abstract;
using DeliveryApp.Shared.Result.ComplexTypes;
using DeliveryApp.Shared.Result.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Concrete
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public AddressService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IResult> AddAsync(AddressAddDto address,string userEmail)
        {
            var add = _mapper.Map<Adress>(address);
            var user = await _userManager.FindByEmailAsync(userEmail);
            add.User = user;
            add.UserId = user.Id;
            await _unitOfWork.Address.AddAsync(add);
            await _unitOfWork.CommitAsync();
            return new Result(ResultStatus.Succes, "Address successfully added");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var address = await _unitOfWork.Address.GetAsync(x => x.Id == id);
            if (address == null)
                return new Result(ResultStatus.Error, "The specified address was not found.");
            await _unitOfWork.Address.DeleteAsync(address);
            await _unitOfWork.CommitAsync();
            return new Result(ResultStatus.Succes, "The specified address deleted.");
        }

        public async Task<IDataResult<IList<AddressDto>>> GetAllAsync()
        {
            var response =await _unitOfWork.Address.GetAllAsync();
            if (response == null)
                return new DataResult<IList<AddressDto>>(ResultStatus.Error, null);
            var addresses = _mapper.Map<IList<AddressDto>>(response);
            return new DataResult<IList<AddressDto>>(ResultStatus.Succes, addresses);
        }

        public async Task<IDataResult<AddressDto>> GetAsync(int id)
        {
            var response = await _unitOfWork.Address.GetAsync(x => x.Id == id);
            if (response == null)
                return new DataResult<AddressDto>(ResultStatus.Error, null);
            return new DataResult<AddressDto>(ResultStatus.Succes, _mapper.Map<AddressDto>(response));
        }

        public async Task<IDataResult<AddressDto>> GetWithUserIdAsync(int userId)
        {
            var response = await _unitOfWork.Address.GetAsync(x => x.UserId == userId);
            if (response == null)
                return new DataResult<AddressDto>(ResultStatus.Error, null);
            var map = _mapper.Map<AddressDto>(response);
            return new DataResult<AddressDto>(ResultStatus.Succes,map);
        }

        public async Task<IResult> UpdateAsync(AddressUpdateDto addressUpdateDto, string userEmail)
        {
            var address = _mapper.Map<Adress>(addressUpdateDto);
            var user = await _userManager.FindByEmailAsync(userEmail);
            address.User = user;
            address.UserId = user.Id;
            await _unitOfWork.Address.UpdateAsync(address);
            await _unitOfWork.CommitAsync();
            return new Result(ResultStatus.Succes, "The specified address updated.");
        }
    }
}
