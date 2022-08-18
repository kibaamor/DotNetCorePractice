using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;

namespace ConfigurationCustomDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddMyConfiguration();

            var configurationRoot = builder.Build();
            ChangeToken.OnChange(() => configurationRoot.GetReloadToken(), () => {
                Console.WriteLine(configurationRoot["lastTime"]);
            });

            Console.ReadKey();
        }
    }
}
