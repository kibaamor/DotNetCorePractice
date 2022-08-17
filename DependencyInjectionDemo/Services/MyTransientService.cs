using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Services
{
    public interface IMyTransientService { }

    public class MyTransientService : IMyTransientService, IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine($"{nameof(MyTransientService)} Dispose");
        }
    }
}
