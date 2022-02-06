using DeliveryApp.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Models
{
    public class ProductWithRecomendedModelView
    {
        public ProductDto Product { get; set; }
        public IList<ProductDto> RecomendedProducts { get; set; }
    }
}
