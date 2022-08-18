using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareDemo.Middlewares
{
    public class MyMiddlewares
    {
        RequestDelegate next;
        ILogger logger;

        public MyMiddlewares(RequestDelegate next, ILogger<MyMiddlewares> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (logger.BeginScope("TraceIdentifier:{TraceIdentifier}", context.TraceIdentifier))
            {
                logger.LogInformation("begin execute");
                await next(context);
                logger.LogInformation("end execute");
            }
        }
    }
}
