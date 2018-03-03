using FindingImmo.Core.Domain.DataAccess;
using FindingImmo.Core.Infrastructure.Logging;
using FindingImmo.Core.Scraping;
using FindingImmo.Core.Scraping.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FindingImmo.Core.Infrastructure.DependencyInjection
{
    public static class DependenciesConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            ConfigureInfratructure(services);
            ConfigureDataAccess(services);
            ConfigureServices(services);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAdsScrapingService, AdsScrapingService>();
        }

        private static void ConfigureDataAccess(IServiceCollection services)
        {
            services.AddTransient<IAdRepository, AdRepository>();
            services.AddDbContext<ImmoDbContext>(options => options.UseSqlite(Configuration.ConnectionString));
        }

        private static void ConfigureInfratructure(IServiceCollection services)
        {
            services.AddSingleton<ILogger>(Logger.Instance);
        }
    }
}
