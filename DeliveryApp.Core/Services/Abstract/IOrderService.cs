using DeliveryApp.Core.Dtos;
using DeliveryApp.Shared.Result.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Core.Services.Abstract
{
    public interface IOrderService
    {
        Task<IDataResult<OrderDto>> CreateOrderAsync(string basketId,string userEmail);
        Task<IDataResult<IList<OrderListDto>>> GetOrderAsync(string userEmail);
        Task<IDataResult<IList<OrderListDto>>> GetOrdersAsync();
        Task<IResult> OrderCancelAsync(int id);
        Task<IResult> UpdateOrderAsync(OrderUpdateDto orderUpdateDto);
    }
}
