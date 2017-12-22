using FindingImmo.Core.Infrastructure;
using FindingImmo.Core.Scraping;
using FindingImmo.Core.Scraping.LeBonCoin;
using Microsoft.Extensions.DependencyInjection;
using System;
using static System.Console;

namespace FindingImmo.Console
{
    public class Program
    {
        private static IServiceProvider ServiceProvider { get; } = ConfigureServices();

        public static void Main(string[] args)
        {
            ILogger logger = ServiceProvider.GetService<ILogger>();

            try
            {
                IAdsScrapingService scrapingService = ServiceProvider.GetService<IAdsScrapingService>();
                scrapingService.UpdateAll();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            ReadKey();
        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            DependenciesConfiguration.Configure(services);
            return services.BuildServiceProvider();
        }
    }
}
