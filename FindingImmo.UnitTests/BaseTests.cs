using FindingImmo.Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace FindingImmo.UnitTests
{
    public abstract class BaseTests
    {
        private static IServiceProvider ServiceProvider { get; } = Startup.Bootstrap(new ServiceCollection());

        protected T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }

        protected IEnumerable<T> GetServices<T>()
        {
            return ServiceProvider.GetServices<T>();
        }
    }
}
