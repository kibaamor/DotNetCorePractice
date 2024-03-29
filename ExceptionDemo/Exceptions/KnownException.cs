﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionDemo.Exceptions
{
    public class KnownException : IKnownException
    {
        public string Message { get; private set; }

        public int ErrorCode { get; private set; }

        public object[] ErrorData { get; private set; }

        public static readonly IKnownException Unknown = new KnownException { Message = "Unknown Error", ErrorCode = 9999 };

        public static IKnownException FromKnownException(IKnownException exception)
        {
            return new KnownException
            {
                Message = exception.Message,
                ErrorCode = exception.ErrorCode,
                ErrorData = exception.ErrorData
            };
        }
    }
}
