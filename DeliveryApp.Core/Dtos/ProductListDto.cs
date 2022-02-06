using DeliveryApp.Core.Entities.Concrete;
using System.Collections.Generic;

namespace DeliveryApp.Core.Dtos
{
    public class ProductListDto:DtoGetBase
    {
        public IList<ProductDto> Products { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

    }
}
