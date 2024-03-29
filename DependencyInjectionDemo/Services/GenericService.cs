﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Services
{
    public interface IGenericService<T>
    {
        public T Data { get; }
    }

    public class GenericService<T> : IGenericService<T>
    {
        public T Data { get; private set; }

        public GenericService(T data) { Data = data; }
    }
}
