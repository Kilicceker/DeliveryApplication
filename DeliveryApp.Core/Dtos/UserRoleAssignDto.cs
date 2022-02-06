using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Core.Dtos
{
    public class UserRoleAssignDto
    {
        public string UserId { get; set; }
        public IList<string> Roles { get; set; }
    }
}
