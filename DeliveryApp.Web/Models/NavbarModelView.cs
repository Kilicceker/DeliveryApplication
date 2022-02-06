using DeliveryApp.Core.Dtos;
using DeliveryApp.Shared.Result.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Models
{
    public class NavbarModelView
    {
        public UserDto Data { get; set; }
        public Core.Entities.Concrete.CustomerBasket Basket { get; set; }
    }
}
