using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;

namespace ConfigurationFileDemo
{
    class Config
    {
        public string Key1 { get; set; }
        public int Key2 { get; set; }
        private bool Key3 { get; set; } = false;

        public bool GetKey3() => Key3;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "appsettings.json");
            builder.AddJsonFile(path, optional: false, reloadOnChange: true);

            builder.AddJsonFile("non-existfile", optional: true);

            var configurationRoot = builder.Build();
            //while (true)
            //{
            //    Console.WriteLine(configurationRoot["key1"]);
            //    Console.WriteLine(configurationRoot["key2"]);
            //    Console.WriteLine(configurationRoot["key3"]);
            //    Console.ReadKey();
            //}

            //IChangeToken token = configurationRoot.GetReloadToken();
            //token.RegisterChangeCallback(state =>
            //{
            //    Console.WriteLine(configurationRoot["key1"]);
            //    Console.WriteLine(configurationRoot["key2"]);
            //    Console.WriteLine(configurationRoot["key3"]);
            //}, configurationRoot);

            var config = new Config();

            configurationRoot.GetSection("bind").Bind(config, (binderOptions) =>
            {
                binderOptions.BindNonPublicProperties = true;
            });

            ChangeToken.OnChange(() => configurationRoot.GetReloadToken(), () =>
            {
                configurationRoot.GetSection("bind").Bind(config, (binderOptions) =>
                {
                    binderOptions.BindNonPublicProperties = true;
                });

                Console.WriteLine(configurationRoot["key1"]);
                Console.WriteLine(configurationRoot["key2"]);
                Console.WriteLine(configurationRoot["key3"]);
                Console.WriteLine(config.Key1);
                Console.WriteLine(config.Key2);
                Console.WriteLine(config.GetKey3());
            });


            Console.ReadKey();
        }
    }
}
