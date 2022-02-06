

using System.Collections.Generic;

namespace DeliveryApp.Core.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int ProductBrandId { get; set; }
        public string ProductBrand { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductType { get; set; }        
        public string PictureUrl { get; set; }
        public IList<CommentReturnDto> Comments { get; set; }
        public int Rating { get; set; }
        //public virtual int Quantity { get; set; } = 0;
    }
}
