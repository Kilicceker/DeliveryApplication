using DeliveryApp.Core.Entities.Abstaract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DeliveryApp.Core.Entities.Concrete
{
    public class Adress:IEntity
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Neighbourhood { get; set; }
        public string DoorNumber { get; set; }
        public string Defination { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
