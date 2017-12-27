using FindingImmo.Core.Infrastructure;
using FindingImmo.Core.Infrastructure.Logging;
using FindingImmo.Core.Scraping;
using Microsoft.Extensions.DependencyInjection;
using System;
using static System.Console;

namespace FindingImmo.Console
{
    public class Program
    {
        private static IServiceProvider ServiceProvider { get; } = Bootstrap();

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
                logger.Fatal(ex);
            }

            ReadKey();
        }

        private static IServiceProvider Bootstrap()
        {
            return Startup.Bootstrap(new ServiceCollection());
        }
    }
}
