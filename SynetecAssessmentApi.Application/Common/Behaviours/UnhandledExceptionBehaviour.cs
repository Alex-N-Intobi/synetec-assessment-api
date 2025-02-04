﻿using MediatR;
using Microsoft.Extensions.Logging;
using SynetecAssessmentApi.Application.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Application.Common.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        private readonly HashSet<Type> _ignoreExceptions = new HashSet<Type>()
        {
            typeof(ValidationException), typeof(NotFoundEmployeeException)
        };

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                if (!_ignoreExceptions.Contains(ex.GetType()))
                {
                    var requestName = typeof(TRequest).Name;

                    _logger.LogError(ex,
                        "Request: Unhandled Exception for Request {Name} {@Request}"
                        , requestName
                        , request);
                }

                throw;
            }
        }
    }
}
