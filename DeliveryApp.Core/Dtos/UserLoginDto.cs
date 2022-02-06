using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Core.Dtos
{
    public class UserLoginDto
    {
        [DisplayName("E_mail")]
        [Required(ErrorMessage = "E_mail can not be null.")]
        [MaxLength(100, ErrorMessage = "E_mail can not be longer than 100 character.")]
        [MinLength(1, ErrorMessage = "E_mail can not be shorter than 1 character.")]
        public string E_mail { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password can not be null.")]
        [MaxLength(100, ErrorMessage = "Password can not be longer than 100 character.")]
        [MinLength(7, ErrorMessage = "Password can not be shorter than 7 character.")]
        public string Password { get; set; }
    }
}
