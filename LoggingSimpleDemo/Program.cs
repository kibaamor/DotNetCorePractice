using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace LoggingSimpleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "appsettings.json");
            configurationBuilder.AddJsonFile(path, optional: false, reloadOnChange: true);
            var config = configurationBuilder.Build();

            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(p => config);

            services.AddLogging(builder =>
            {
                builder.AddConfiguration(config.GetSection("Logging"));
                builder.AddConsole();
                builder.AddDebug();
            });

            services.AddTransient<OrderService>();

            IServiceProvider provider = services.BuildServiceProvider();

            ILoggerFactory factory = provider.GetService<ILoggerFactory>();

            var logger = factory.CreateLogger("logger");
            logger.LogDebug(2001, "aiya");

            var logger2 = factory.CreateLogger("logger");
            logger2.LogDebug("aiya2");

            var logger3 = provider.GetService<ILogger<Program>>();
            logger.LogInformation(new EventId(201, "xihua"), "hello");

            var orderService = provider.GetService<OrderService>();
            orderService.Show();

            // 日志配置支持动态更新
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                using (logger.BeginScope("ScopeId: {scopeId}", Guid.NewGuid()))
                {
                    logger.LogInformation("information in scope");
                    logger.LogDebug("debug in scope");
                }
            }
        }
    }
}
