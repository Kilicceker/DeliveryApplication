using DeliveryApp.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Models
{
    public class AllProductsViewModel
    {
        public List<ProductTypeWithProductsDto> Products { get; set; } = new List<ProductTypeWithProductsDto>();
    }
}
