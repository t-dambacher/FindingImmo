using FindingImmo.Core.Domain.Models;
using FindingImmo.Core.Infrastructure;
using FindingImmo.Core.Infrastructure.Logging;
using FindingImmo.Core.Scraping;
using FindingImmo.Core.Scraping.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using static System.Console;

namespace FindingImmo.Console
{
    public class Program
    {
        private static IServiceProvider ServiceProvider { get; } = Bootstrap();

        public static void Main(string[] args)
        {
            ILogger logger = ServiceProvider.GetService<ILogger>();
            IAdsScrapingService scrapingService = ServiceProvider.GetService<IAdsScrapingService>();

            try
            {
                IEnumerable<Ad> ads = scrapingService.UpdateAll();
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
