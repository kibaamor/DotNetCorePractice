using DependencyInjectionDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Controllers
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
        private readonly IOrderService _orderService;
        private readonly IGenericService<IOrderService> _genericService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IOrderService orderService, IGenericService<IOrderService> genericService)
        {
            _logger = logger;
            _orderService = orderService;
            _genericService = genericService;

            Console.WriteLine($"IOrderService: {orderService}, {orderService.GetHashCode()}");
            Console.WriteLine($"IGenericService<IOrderService>: {genericService}, {genericService.GetHashCode()}");
            Console.WriteLine($"IOrderService in IGenericService<IOrderService>: {genericService.Data}, {genericService.Data.GetHashCode()}");
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get(
            [FromServices] IMySingletonService mySingleton1,
            [FromServices] IMySingletonService mySingleton2,
            [FromServices] IMyScopedService myScoped1,
            [FromServices] IMyScopedService myScoped2,
            [FromServices] IMyTransientService myTransient1,
            [FromServices] IMyTransientService myTransient2,
            [FromServices] IOrderService orderService1,
            [FromServices] IOrderService orderService2,
            [FromServices] IEnumerable<IOrderService> services)
        {
            Console.WriteLine("vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv");
            Console.WriteLine($"{nameof(mySingleton1)}: {mySingleton1.GetHashCode()}");
            Console.WriteLine($"{nameof(mySingleton2)}: {mySingleton2.GetHashCode()}");
            Console.WriteLine($"{nameof(myScoped1)}: {myScoped1.GetHashCode()}");
            Console.WriteLine($"{nameof(myScoped2)}: {myScoped2.GetHashCode()}");
            Console.WriteLine($"{nameof(myTransient1)}: {myTransient1.GetHashCode()}");
            Console.WriteLine($"{nameof(myTransient2)}: {myTransient2.GetHashCode()}");
            Console.WriteLine($"{nameof(orderService1)}: {orderService1.GetHashCode()}");
            Console.WriteLine($"{nameof(orderService2)}: {orderService2.GetHashCode()}");

            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            foreach (var service in services)
            {
                Console.WriteLine($"IOrderService实例: {service.ToString()}, {service.GetHashCode()}");
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
            return result;
        }
    }
}
