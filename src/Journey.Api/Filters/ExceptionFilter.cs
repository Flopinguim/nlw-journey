using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Journey.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is JourneyException)
            {
                var jouneyException = (JourneyException)context.Exception;

                context.HttpContext.Response.StatusCode = (int)jouneyException.GetStatusCode();

                var responseJson = new ResponseErrosJson(jouneyException.GetErrorMessages());

                context.Result = new ObjectResult(responseJson);
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var responseJson = new ResponseErrosJson([ResourceErrorMessage.UnknownErro]);

                context.Result = new ObjectResult(responseJson);
            }
        }
    }
}
