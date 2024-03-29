﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Services
{
    public interface IMySingletonService { }

    public class MySingletonService : IMySingletonService, IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine($"{nameof(MySingletonService)} Dispose");
        }
    }
}
