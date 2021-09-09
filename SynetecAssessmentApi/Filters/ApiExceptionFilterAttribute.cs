using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SynetecAssessmentApi.Application.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {

        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilterAttribute()
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundEmployeeException), HandleNotFoundEmployeeException },
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = (ValidationException)context.Exception;

            var details = new ValidationProblemDetails(exception.Errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleNotFoundEmployeeException(ExceptionContext context)
        {
            var exception = (NotFoundEmployeeException)context.Exception;

            //var details = new ProblemDetails()
            //{
            //    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            //    Title = "The specified resource was not found.",
            //    Detail = exception.Message
            //};

            //context.Result = new NotFoundObjectResult(details);

            var details = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "The specified resource was not found.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Detail = exception.Message,
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };

            context.ExceptionHandled = true;
        }
    }
}
