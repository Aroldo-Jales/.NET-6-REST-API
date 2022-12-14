using System.Net;

namespace Prova1.Application.Common.Errors.Authentication;

public class DuplicateEmailException : Exception, IExceptionBase
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public string ErrorMessage => "Email already exists.";
}