namespace Core.Models.Abstract;

public interface IServiceResponse
{
    public string ExceptionMessage { get; set; }

    public  bool Success { get; set; }

    public bool HasError { get; set; }
}