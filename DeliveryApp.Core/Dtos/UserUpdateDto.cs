using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace DeliveryApp.Core.Dtos
{
    public class UserUpdateDto
    {

        [DisplayName("User_id")]
        [Required(ErrorMessage = "Id can not be null.")]
        public int Id { get; set; }

        [DisplayName("E_mail")]
        [Required(ErrorMessage = "E_mail can not be null.")]
        [MaxLength(100, ErrorMessage = "E_mail can not be longer than 100 character.")]
        [MinLength(1, ErrorMessage = "E_mail can not be shorter than 1 character.")]
        public string Email { get; set; }

        [DisplayName("Telephone_number")]
        [Required(ErrorMessage = "Telephone_number can not be null.")]
        [MaxLength(15, ErrorMessage = "Telephone_number can not be longer than 15 character.")]
        [MinLength(10, ErrorMessage = "Telephone_number can not be shorter than 10 character.")]
        public string PhoneNumber { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name can not be null.")]
        [MaxLength(100, ErrorMessage = "Name can not be longer than 100 character.")]
        [MinLength(1, ErrorMessage = "Name can not be shorter than 1 character.")]
        public string UserName { get; set; }

        [DisplayName("Surname")]
        [Required(ErrorMessage = "Surname can not be null.")]
        [MaxLength(100, ErrorMessage = "Surname can not be longer than 100 character.")]
        [MinLength(1, ErrorMessage = "Surname can not be shorter than 1 character.")]
        public string UserSurname { get; set; }
    }
}
