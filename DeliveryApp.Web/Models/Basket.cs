using DeliveryApp.Core.Entities.Concrete;
using DeliveryApp.Shared.Result.ComplexTypes;
using System;

namespace DeliveryApp.Web.Models
{
    public class Basket
    {

        public CustomerBasket Data { get; set; }

        public ResultStatus ResultStatus { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }


    }
}
