// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.Extensions.Logging;
using System;

namespace UnitTests.Common
{
    public class FakeLogger : ILogger
    {
        class FakeDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new FakeDisposable();
        }
    }

    public class FakeLogger<T> : ILogger<T>
    {
        class FakeDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new FakeDisposable();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
        }
    }
}
