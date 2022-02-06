using DeliveryApp.Shared.Result.Abstract;
using DeliveryApp.Shared.Result.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Shared.Result.Concrete
{
    public class DataResult<T> : IDataResult<T>
    {
        public DataResult(ResultStatus _resultStatus, T data)
        {
            ResultStatus = _resultStatus;
            Data = data;
        }
        public DataResult(ResultStatus _resultStatus, string message, T data)
        {
            ResultStatus = _resultStatus;
            Message = message;
            Data = data;
        }
        public DataResult(ResultStatus _resultStatus, string message, T data, Exception exception)
        {
            ResultStatus = _resultStatus;
            Message = message;
            Data = data;
            Exception = exception;
        }
        public T Data { get; }

        public ResultStatus ResultStatus { get; }

        public string Message { get; }

        public Exception Exception { get; }
    }
}
