using DependencyInjectionScopeAndDisposableDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionScopeAndDisposableDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get(
            [FromServices] IOrderService orderService1,
            [FromServices] IOrderService orderService2,
            [FromServices] IHostApplicationLifetime hostApplicationLifetime,
            [FromQuery] bool stop = false)
        {
            Console.WriteLine("vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv");
            Console.WriteLine($"{nameof(orderService1)}: {orderService1.GetHashCode()}");
            Console.WriteLine($"{nameof(orderService2)}: {orderService2.GetHashCode()}");

            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            // HttpContext.RequestServices 指当前请求的容器
            using (IServiceScope scope = HttpContext.RequestServices.CreateScope())
            {
                var service1 = scope.ServiceProvider.GetService<IOrderService>();
                var service2 = scope.ServiceProvider.GetService<IOrderService>();
                Console.WriteLine($"{nameof(service1)}: {service1.GetHashCode()}");
                Console.WriteLine($"{nameof(service2)}: {service2.GetHashCode()}");
            }

            var rng = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
            Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");

            if (stop)
            {
                hostApplicationLifetime.StopApplication();
            }

            return result;
        }
    }
}
