using Task_manager.Enum;


namespace Task_manager.Domain.Respone
{
    public class BaseRespone<T> : IBaseRespone<T>
    {
        public string Description { get ; set; }
        public StatusCode StatusCode { get; set; }
        public T Data { get; set; }
    }
}

public interface IBaseRespone<T>
{
    string Description { get; set; }
    StatusCode StatusCode { get; set; }

    T Data { get; set; }
}

