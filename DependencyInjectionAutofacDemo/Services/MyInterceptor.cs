using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionAutofacDemo.Services
{
    public class MyInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"Intercept before method: {invocation.Method.Name}");
            invocation.Proceed();
            Console.WriteLine($"Intercept after method: {invocation.Method.Name}");
        }
    }
}
