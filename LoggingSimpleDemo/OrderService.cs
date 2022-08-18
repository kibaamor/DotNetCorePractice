using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoggingSimpleDemo
{
    class OrderService
    {
        ILogger<OrderService> logger;

        public OrderService(ILogger<OrderService> logger)
        {
            this.logger = logger;
        }

        public void Show()
        {
            logger.LogDebug("[Debug] Show Time: {time}", DateTime.Now);
            logger.LogInformation("[Information] Show Time: {time}", DateTime.Now);
        }
    }
}
