using FindingImmo.Core.Domain.DataAccess;
using FindingImmo.Core.Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FindingImmo.Core.Infrastructure
{
    public static class Startup
    {
        public static IServiceProvider Bootstrap(IServiceCollection services)
        {
            Configuration.Bootstrap();
            DependenciesConfiguration.Configure(services);

            IServiceProvider provider = services.BuildServiceProvider();

            ImmoDbContext.EnsureCreated(provider.GetService<DbContextOptions<ImmoDbContext>>());

            return provider;
        }
    }
}
