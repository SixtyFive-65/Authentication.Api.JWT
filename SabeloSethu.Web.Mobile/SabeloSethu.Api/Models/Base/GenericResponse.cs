using System.Net;

namespace SabeloSethu.Api.Models.Base
{
    public class GenericResponse<T>
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public List<string> ErrorMessage { get; set; } = [];

        //public GenericResponse(bool isSuccess, HttpStatusCode httpStatusCode, T data, string message, List<string> errorMessage)
        //{
        //    IsSuccess = isSuccess;
        //    HttpStatusCode = httpStatusCode;
        //    Data = data;
        //    Message = message;
        //    ErrorMessage = errorMessage;
        //}
    }
}
