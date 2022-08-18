using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace ConfigurationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(new Dictionary<string, string> {
                {"key1", "value1"},
                {"key2", "value2"},
                {"section1:key3", "value3"},
            });

            IConfigurationRoot configurationRoot = builder.Build();

            IConfiguration configuration = configurationRoot;

            Console.WriteLine(configuration["key1"]);
            Console.WriteLine(configuration["key2"]);

            IConfigurationSection section1 = configuration.GetSection("section1");
            Console.WriteLine(section1["key3"]);
        }
    }
}
