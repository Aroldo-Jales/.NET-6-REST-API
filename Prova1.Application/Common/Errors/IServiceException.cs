using System.Net;

namespace Prova1.Application.Common.Errors
{
    public interface IExceptionBase
    {
        public HttpStatusCode StatusCode { get; }
        public string ErrorMessage { get; }
    }
}