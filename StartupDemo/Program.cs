using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    Console.WriteLine("ConfigureWebHostDefaults");
                    webBuilder.UseStartup<Startup>();

                    webBuilder.ConfigureServices(services =>
                    {
                        Console.WriteLine("ConfigureServices in ConfigureWebHostDefaults");
                    });
                    webBuilder.Configure(builder =>
                    {
                        Console.WriteLine("Configure in ConfigureWebHostDefaults");
                    });
                })
                .ConfigureServices(builder =>
                {
                    Console.WriteLine("ConfigureServices");
                })
                .ConfigureLogging(builder =>
                {
                    Console.WriteLine("ConfigureLogging");
                })
                .ConfigureAppConfiguration(builder =>
                {
                    Console.WriteLine("ConfigureAppConfiguration");
                })
                .ConfigureHostConfiguration(builder =>
                {
                    Console.WriteLine("ConfigureHostConfiguration");
                });
    }
}
