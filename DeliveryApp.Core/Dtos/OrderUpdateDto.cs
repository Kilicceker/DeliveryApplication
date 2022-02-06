using DeliveryApp.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Core.Dtos
{
    public class OrderUpdateDto
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public bool IsCanceled { get; set; }
    }
}
