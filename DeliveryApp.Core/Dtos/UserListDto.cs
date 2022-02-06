using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DeliveryApp.Core.Dtos
{
    public class UserListDto
    {
        public int Id { get; set; }
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

        public string Email { get; set; }
        [DisplayName("Roles")]
        [Required(ErrorMessage = "Roles can not be null.")]
        [MaxLength(100, ErrorMessage = "Role can not be longer than 100 character.")]
        [MinLength(1, ErrorMessage = "Role can not be shorter than 1 character.")]
        public IList<string> Roles { get; set; }

        [DisplayName("Telephone_number")]
        [Required(ErrorMessage = "Telephone_number can not be null.")]
        [MaxLength(15, ErrorMessage = "Telephone_number can not be longer than 15 character.")]
        [MinLength(10, ErrorMessage = "Telephone_number can not be shorter than 10 character.")]
        public string PhoneNumber { get; set; }
    }
}
