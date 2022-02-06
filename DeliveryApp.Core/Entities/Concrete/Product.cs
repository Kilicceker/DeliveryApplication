using DeliveryApp.Core.Entities.Abstaract;
using System.Collections.Generic;

namespace DeliveryApp.Core.Entities.Concrete
{
    public class Product:EntityBase,IEntity
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
        public int ProductBrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Order> Orders { get; set; }
        public virtual int Rating { get; set; } = 0;
        public virtual int RatingCount { get; set; } = 0;
    }
}
