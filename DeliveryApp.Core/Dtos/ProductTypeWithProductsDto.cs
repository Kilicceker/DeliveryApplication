using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Core.Dtos
{
    public class ProductTypeWithProductsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<ProductDto> Products { get; set; }
    }
}
