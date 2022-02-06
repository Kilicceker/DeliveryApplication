using DeliveryApp.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Core.Dtos
{
    public class OrderDto
    {

        IList<ProductDto> Products { get; set; }
        public string DeliverAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual OrderStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
