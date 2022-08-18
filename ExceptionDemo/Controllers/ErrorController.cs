using ExceptionDemo.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionDemo.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/error")]
        public IActionResult Index()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            var ex = exceptionHandlerPathFeature?.Error;
            
            var knownException = ex as IKnownException;
            if (knownException == null)
            {
                var logger = HttpContext.RequestServices.GetService<ILogger<ErrorController>>();
                logger.LogError(ex, ex.Message);
                knownException = KnownException.Unknown;
            }
            else
            {
                knownException = KnownException.FromKnownException(knownException);
            }
            return View(knownException);
        }
    }
}
