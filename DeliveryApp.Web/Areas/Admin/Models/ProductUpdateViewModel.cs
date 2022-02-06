using DeliveryApp.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Areas.Admin.Models
{
    public class ProductUpdateViewModel
    {
        public IList<ProductTypeDto> Categories { get; set; }
        public IList<ProductBrandDto> Brands { get; set; }
        public ProductDto Product { get; set; }
    }
}
