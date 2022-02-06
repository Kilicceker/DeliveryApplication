using DeliveryApp.Core.Dtos;
using DeliveryApp.Web.Areas.Admin.Models;
using DeliveryApp.Web.Models;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Services
{
    public interface IOrderService
    {
        Task<Order> GetOrdersAsync(string url);
        Task CreateOrdersAsync(CreateOrderDto createOrderDto,string url);
        Task UpdateOrderAsync(OrderUpdateDto orderUpdateDto,string url);
    }
}
