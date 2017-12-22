using FindingImmo.Core.Domain.Data;
using FindingImmo.Core.Domain.DataAccess;
using FindingImmo.Core.Scraping;
using FindingImmo.Core.Scraping.LeBonCoin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FindingImmo.Core.Infrastructure
{
    public static class DependenciesConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<ILogger>(Logger.Instance);
            services.AddTransient<IAdsScrapingService, AdsScrapingService>();
            services.AddTransient<AdsProvider, LeBoinCoinAdProvider>();
            services.AddTransient<LeBonCoinAdReferencesScraper>();
            services.AddTransient<LeBonCoinAdScraper>();
            services.AddDbContext<ImmoDbContext>(options => options.UseSqlite(Configuration.ConnectionString));
            services.AddTransient<IAdRepository, AdRepository>();
        }
    }
}
