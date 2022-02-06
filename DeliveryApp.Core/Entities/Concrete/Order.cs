using DeliveryApp.Core.Entities.Abstaract;
using System;
using System.Collections.Generic;

namespace DeliveryApp.Core.Entities.Concrete
{
    public class Order:IEntity
    {
        public Order()
        {

        }
        public Order(ICollection<Product> products, string deliverAddress, int userId, User user, decimal totalPrice,string quantities)
        {
            Products = products;
            DeliverAddress = deliverAddress;
            UserId = userId;
            User = user;
            Quantities = quantities;
            TotalPrice = totalPrice;
        }

        public int Id { get; set; }
        public ICollection<Product> Products { get; set; }
        public string Quantities { get; set; }
        public string DeliverAddress { get; set; }
        public virtual OrderStatus Status { get; set; } = OrderStatus.Preparing;
        public virtual bool IsCanceled { get; set; } = false;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public User User { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
