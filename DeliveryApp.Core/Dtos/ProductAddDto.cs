using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DeliveryApp.Core.Dtos
{
    public class ProductAddDto
    {

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name can not be null.")]
        [MaxLength(100, ErrorMessage = "Name can not be longer than 100 character.")]
        [MinLength(1, ErrorMessage = "Name can not be shorter than 1 character.")]
        public string Name { get; set; }

        [DisplayName("Price")]
        [Required(ErrorMessage = "Price can not be null and can not be smaller than zero.")]
         public decimal Price { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Description is can not be null.")]
        [MinLength(10, ErrorMessage = "Description can not be shorter than 10 character.")]
        [MaxLength(500, ErrorMessage = "Description can not be longer than 500 character.")]
        public string Description { get; set; }

        [DisplayName("Brand Id")]
        [Required(ErrorMessage = "BrandId can not be null.")]
        public int ProductBrandId { get; set; }

        [DisplayName("Type Id")]
        [Required(ErrorMessage = "TypeId can not be null.")]
        public int ProductTypeId { get; set; }

        [DisplayName("Picture Url")]
        [Required(ErrorMessage = "PictureUrl can not be null.")]
        [MaxLength(250, ErrorMessage = "PictureUrl can not be longer than 250 character.")]
        [MinLength(5, ErrorMessage = "PictureUrl can not be shorter than 5 character.")]
         public string PictureUrl { get; set; }
    }
}
