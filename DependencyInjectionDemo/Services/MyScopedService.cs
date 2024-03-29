﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Services
{
    public interface IMyScopedService { }

    public class MyScopedService : IMyScopedService, IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine($"{nameof(MyScopedService)} Dispose");
        }
    }
}
