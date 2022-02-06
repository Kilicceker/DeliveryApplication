using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Core.Entities.Concrete
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Preparing")]
        Preparing,

        [EnumMember(Value = "In delivery")]
        InDelivery,

        [EnumMember(Value = "Delivered")]
        Delivered
    }
}
