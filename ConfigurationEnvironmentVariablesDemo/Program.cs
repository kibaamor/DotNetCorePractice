using Microsoft.Extensions.Configuration;
using System;

namespace ConfigurationEnvironmentVariablesDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables("K_");
            
            var configurationRoot = builder.Build();
            Console.WriteLine(configurationRoot["key1"]);

            var section1 = configurationRoot.GetSection("section1");
            Console.WriteLine(section1["key2"]);

            var section2 = configurationRoot.GetSection("section1:section2");
            Console.WriteLine(section2["key3"]);
        }
    }
}
