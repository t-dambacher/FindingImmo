using FindingImmo.Core.Domain.DataAccess;
using FindingImmo.Core.Featurization;
using FindingImmo.Core.Featurization.Evaluators;
using FindingImmo.Core.Infrastructure.Logging;
using FindingImmo.Core.Scraping;
using FindingImmo.Core.Scraping.LeBonCoin;
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
            services.AddTransient<AdsProvider, LeBoinCoinAdProvider>();
            services.AddTransient<LeBonCoinAdReferencesScraper>();
            services.AddTransient<LeBonCoinAdScraper>();
            services.AddTransient<IFeaturesService, FeaturesService>();

            ConfigureFeatures(services);
        }


        private static void ConfigureFeatures(IServiceCollection services)
        {
            services.AddSingleton<TwoFamilyFeatureEvaluator>();
        }

        private static void ConfigureDataAccess(IServiceCollection services)
        {
            services.AddTransient<IAdRepository, AdRepository>();
            services.AddTransient<IFeaturesRepository, FeaturesRepository>();
            services.AddDbContext<ImmoDbContext>(options => options.UseSqlite(Configuration.ConnectionString));
        }

        private static void ConfigureInfratructure(IServiceCollection services)
        {
            services.AddSingleton<ILogger>(Logger.Instance);
        }
    }
}
