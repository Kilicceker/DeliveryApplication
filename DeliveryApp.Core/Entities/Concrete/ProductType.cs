using DeliveryApp.Core.Entities.Abstaract;
using System.Collections.Generic;

namespace DeliveryApp.Core.Entities.Concrete
{
    public class ProductType:EntityBase,IEntity
    {
        public ICollection<Product> Products { get; set; }
        public string PictureUrl { get; set; }
    }
}
