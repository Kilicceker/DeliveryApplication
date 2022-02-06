using DeliveryApp.Core.Entities.Abstaract;
using Microsoft.AspNetCore.Identity;

namespace DeliveryApp.Core.Entities.Concrete
{
    public class RoleClaim : IdentityRoleClaim<int>, IEntity
    {
    }
}