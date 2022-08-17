using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionScopeAndDisposableDemo.Services
{
    public interface IOrderService {}

    public class OrderService : IOrderService, IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine($"{nameof(OrderService)} Dispose: {this.GetHashCode()}");
        }
    }
}
