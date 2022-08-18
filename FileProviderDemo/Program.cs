using Microsoft.Extensions.FileProviders;
using System;

namespace FileProviderDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IFileProvider provider1 = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory);
            foreach (var file in provider1.GetDirectoryContents("/"))
            {
                Console.WriteLine(file.Name);
            }

            Console.WriteLine("---------------------");

            IFileProvider provider2 = new EmbeddedFileProvider(typeof(Program).Assembly);
            var html = provider2.GetFileInfo("emb.html");
            Console.WriteLine(html.Name);

            Console.WriteLine("---------------------");

            IFileProvider provider3 = new CompositeFileProvider(provider1, provider2);
            foreach (var file in provider3.GetDirectoryContents("/"))
            {
                Console.WriteLine(file.Name);
            }
        }
    }
}
