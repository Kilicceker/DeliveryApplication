using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Core.Dtos
{
    public class AddressUpdateDto
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Neighbourhood { get; set; }
        public string DoorNumber { get; set; }
        public string Defination { get; set; }
    }
}
