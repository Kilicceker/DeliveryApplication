using DeliveryApp.Core.Entities.Abstaract;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DeliveryApp.Core.Entities.Concrete
{
    public class User:IdentityUser<int>,IEntity
    {
        public Adress Adresses { get; set; }
        public string UserSurname { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
