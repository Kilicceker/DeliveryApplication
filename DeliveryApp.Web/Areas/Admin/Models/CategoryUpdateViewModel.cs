using DeliveryApp.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Areas.Admin.Models
{
    public class CategoryUpdateViewModel
    {
        public ProductTypeUpdateDto ProductTypeUpdateDto { get; set; }
        public ProductTypeDto ProductTypeDto { get; set; }
    }
}
