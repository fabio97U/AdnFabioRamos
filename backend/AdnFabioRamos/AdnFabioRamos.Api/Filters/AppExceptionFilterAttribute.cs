using System;
using System.Net;
using AdnFabioRamos.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace AdnFabioRamos.Api.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class AppExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<Exception> _Logger;

        public AppExceptionFilterAttribute(ILogger<Exception> logger)
        {
            _Logger = logger;
        }


        public override void OnException(ExceptionContext context)
        {
            if (context != null)
            {
                switch (context.Exception)
                {
                    case AppException:
                        context.HttpContext.Response.StatusCode = ((int)HttpStatusCode.BadRequest);
                        return;
                    default:
                        context.HttpContext.Response.StatusCode = ((int)HttpStatusCode.InternalServerError);
                        break;
                }

                _Logger.LogError(context.Exception, context.Exception.Message, new[] { context.Exception.StackTrace });

                var msg = new
                {
                    context.Exception.Message
                };

                context.Result = new ObjectResult(msg);
            }
        }

    }
}
