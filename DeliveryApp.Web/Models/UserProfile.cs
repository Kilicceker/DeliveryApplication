using DeliveryApp.Core.Dtos;
using System.Collections.Generic;

namespace DeliveryApp.Web.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
        public IList<OrderListDto> Orders { get; set; }
        public AddressDto Address { get; set; }
    }
}
