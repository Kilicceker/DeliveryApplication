namespace DeliveryApp.Shared.Result.Abstract
{
    public interface IDataResult<out T>:IResult
    {
        public T Data { get; }
    }
}
