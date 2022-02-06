using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Core.Dtos
{
    public class PagginationDto
    {
        public int? ProductTypeId { get; set; }
        public int? ProductBrandId { get; set; }
        public int CurrentPage { get; set; }
        public virtual int PageSize { get; set; } = 10;
        public virtual bool IsAscending { get; set; } = false;
    }
}
