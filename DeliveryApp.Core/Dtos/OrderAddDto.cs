using DeliveryApp.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Core.Dtos
{
    public class OrderAddDto
    {
        public ICollection<Product> Products { get; set; }
        public string Quantities { get; set; }
        public string DeliverAddress { get; set; }
        public virtual OrderStatus Status { get; set; }
        public virtual bool IsCanceled { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
