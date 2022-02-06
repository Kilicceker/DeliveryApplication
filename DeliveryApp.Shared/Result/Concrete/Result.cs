using DeliveryApp.Shared.Result.Abstract;
using DeliveryApp.Shared.Result.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Shared.Result.Concrete
{
    public class Result : IResult
    {
        public ResultStatus ResultStatus { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }

        public Result(ResultStatus _resultStatus)
        {
            ResultStatus = _resultStatus;
        }
        public Result(ResultStatus _resultStatus, string message)
        {
            ResultStatus = _resultStatus;
            Message = message;
        }
        public Result(ResultStatus _resultStatus, string message, Exception exception)
        {
            ResultStatus = _resultStatus;
            Message = message;
            Exception = exception;
        }


    }
}
