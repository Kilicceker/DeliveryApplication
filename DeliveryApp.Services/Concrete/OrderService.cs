using AutoMapper;
using DeliveryApp.Core.Dtos;
using DeliveryApp.Core.Entities.Concrete;
using DeliveryApp.Core.Repositories.Abstract;
using DeliveryApp.Core.Services.Abstract;
using DeliveryApp.Core.UnitOfWorks;
using DeliveryApp.Shared.Result.Abstract;
using DeliveryApp.Shared.Result.ComplexTypes;
using DeliveryApp.Shared.Result.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IAddressService _addressService;
        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper, IAddressService addressService)
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            _addressService = addressService;
        }


        public async Task<IDataResult<OrderDto>> CreateOrderAsync(string basketId,string userEmail)
        {
            var basket =await _basketRepo.GetBasketAsync(basketId);
            var productList = new List<Product>();
            string quantities = "";
            foreach (var product in basket.Items)
            {
                productList.Add(await _unitOfWork.Products.GetAsync(x => x.Id == product.Id));
                quantities += product.Quantity;
            }
            var user = await _userManager.FindByEmailAsync(userEmail);
            var address = await _addressService.GetWithUserIdAsync(user.Id);
            var deliveryAddress = address.Data.Neighbourhood + " " + address.Data.Street + " " + " " + address.Data.DoorNumber + " " + address.Data.City;
            Order order = new Order(productList,deliveryAddress,user.Id,user,basket.TotalPrice,quantities);
            try
            {
                await _unitOfWork.Order.AddAsync(order);
            }
            catch (Exception e)
            {

            }
            var returnDto=_mapper.Map<OrderDto>(order);
            await _unitOfWork.CommitAsync();
            await _basketRepo.DeleteBasketAsync(user.Id.ToString());
            return new DataResult<OrderDto>(ResultStatus.Succes, returnDto);
        }

        public async Task<IDataResult<IList<OrderListDto>>> GetOrderAsync(string userEmail)
        {
            var user= await _userManager.FindByEmailAsync(userEmail);
            var response = await _unitOfWork.Order.GetAllAsync(x=>x.UserId==user.Id,x=>x.Products);
            if (response == null)
                return new DataResult<IList<OrderListDto>>(ResultStatus.Error, null);
            var orders = _mapper.Map<IList<OrderListDto>>(response);
            var quantities = new List<string>();
            foreach (var item in response)
            {
                quantities.Add(item.Quantities);
            }
            return new DataResult<IList<OrderListDto>>(ResultStatus.Succes, orders);
        }

        public async Task<IDataResult<IList<OrderListDto>>> GetOrdersAsync()
        {
            var response = await _unitOfWork.Order.GetAllAsync(null,x=>x.Products);
            if (response == null)
                return new DataResult<IList<OrderListDto>>(ResultStatus.Error, null);
            var orders = _mapper.Map<IList<OrderListDto>>(response);
            return new DataResult<IList<OrderListDto>>(ResultStatus.Succes, orders);
        }

        public async Task<IResult> OrderCancelAsync(int id)
        {
            var response = await _unitOfWork.Order.GetAsync(x => x.Id==id);
            if (response.Status == OrderStatus.Delivered || response.Status == OrderStatus.InDelivery || response==null)
                return new Result(ResultStatus.Info, "You cannot cancel your order that has been delivered or delivered.");
            response.IsCanceled = true;
            await _unitOfWork.Order.UpdateAsync(response);
            await _unitOfWork.CommitAsync();
            return new Result(ResultStatus.Succes, "Your order has been canceled");
        }
        public async Task<IResult> UpdateOrderAsync(OrderUpdateDto orderUpdateDto)
        {
            var response = await _unitOfWork.Order.GetAsync(x => x.Id == orderUpdateDto.Id,x=>x.Products);
            if (response==null)
                return new Result(ResultStatus.Error, "There is no any order specified criteria");
            response.IsCanceled = orderUpdateDto.IsCanceled;
            response.Status = orderUpdateDto.Status;
            await _unitOfWork.Order.UpdateAsync(response);
            await _unitOfWork.CommitAsync();
            return new Result(ResultStatus.Succes, "Your order has been updated");
        }
    }
}
