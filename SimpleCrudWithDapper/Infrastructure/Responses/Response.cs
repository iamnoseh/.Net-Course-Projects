using System.Net;
namespace Infrastructure.Responses;

public class Response<T>
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public Response(T data, HttpStatusCode statusCode, string message)
    {
        Data = data;
        StatusCode = (int)statusCode;
        Message = message;
    }

    public Response(HttpStatusCode statusCode, string message)
    {
        StatusCode = (int)statusCode;
        Message = message;
        Data = default(T);
    }

    public Response(T data)
    {
        StatusCode = (int)HttpStatusCode.OK;
        Data = data;
        Message = "Success";
    }
}