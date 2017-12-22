using FindingImmo.Core.Scraping;
using FindingImmo.Core.Scraping.LeBonCoin;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

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
        }
    }
}
