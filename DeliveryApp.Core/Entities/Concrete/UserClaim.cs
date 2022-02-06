using DeliveryApp.Core.Entities.Abstaract;
using Microsoft.AspNetCore.Identity;

namespace DeliveryApp.Core.Entities.Concrete
{
    public class UserClaim : IdentityUserClaim<int>, IEntity
    {
    }
}