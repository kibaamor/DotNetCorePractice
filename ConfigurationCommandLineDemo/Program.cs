using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace ConfigurationCommandLineDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            
            //builder.AddCommandLine(args);

            // 命令替换
            var mapper = new Dictionary<string, string>
            {
                {"-k1", "key1" },
            };
            builder.AddCommandLine(args, mapper);

            var configurationRoot = builder.Build();
            Console.WriteLine($"key1: {configurationRoot["key1"]}");
            Console.WriteLine($"key2: {configurationRoot["key2"]}");
            Console.WriteLine($"key3: {configurationRoot["key3"]}");
        }
    }
}
