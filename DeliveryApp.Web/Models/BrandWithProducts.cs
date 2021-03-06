using DeliveryApp.Core.Dtos;
using DeliveryApp.Shared.Result.ComplexTypes;
using System;
using System.Collections.Generic;

namespace DeliveryApp.Web.Models
{
    public class BrandWithProducts
    {
        public ProductBrandWithProductsDto Data { get; set; }

        public ResultStatus ResultStatus { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }
    }
}
