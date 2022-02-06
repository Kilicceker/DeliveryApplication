using DeliveryApp.Core.Entities.Abstaract;
using System.Collections.Generic;

namespace DeliveryApp.Core.Entities.Concrete
{
    public class ProductBrand:EntityBase,IEntity
    {
        public ICollection<Product> Products { get; set; }
    }
}
