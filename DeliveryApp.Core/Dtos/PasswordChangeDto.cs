using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Core.Dtos
{
    public class PasswordChangeDto
    {
        [DisplayName("Password")]
        [Required(ErrorMessage = "Password can not be null.")]
        [MaxLength(100, ErrorMessage = "Password can not be longer than 100 character.")]
        [MinLength(7, ErrorMessage = "Password can not be shorter than 7 character.")]
        public string CurrentPassword { get; set; }

        [DisplayName("New_Password")]
        [Required(ErrorMessage = "New_Password can not be null.")]
        [MaxLength(100, ErrorMessage = "New_Password can not be longer than 100 character.")]
        [MinLength(7, ErrorMessage = "New_Password can not be shorter than 7 character.")]
        public string NewPassword { get; set; }

        [DisplayName("NewPassword_repeat")]
        [Required(ErrorMessage = "NewPassword_repeat can not be null.")]
        [MaxLength(100, ErrorMessage = "NewPassword_repeat can not be longer than 100 character.")]
        [MinLength(7, ErrorMessage = "NewPassword_repeat can not be shorter than 7 character.")]
        public string RepeatPassword { get; set; }
    }
}
