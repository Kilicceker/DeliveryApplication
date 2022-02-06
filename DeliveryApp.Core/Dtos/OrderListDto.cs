using DeliveryApp.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Core.Dtos
{
    public class OrderListDto
    {
        public int Id { get; set; }
        public IList<OrderedProductDto> Products { get; set; }
        public string DeliverAddress { get; set; }
        public string Quantities { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
