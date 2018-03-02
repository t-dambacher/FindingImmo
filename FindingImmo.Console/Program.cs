using FindingImmo.Core.Infrastructure.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                Stopwatch watch = Stopwatch.StartNew();
                IEnumerable<Ad> ads = scrapingService.UpdateAll();
                watch.Stop();

                WriteLine($"Elapsed time for scraping: {watch.Elapsed.TotalMinutes} minutes.");

                INlpService nlpService = ServiceProvider.GetService<INlpService>();

                watch.Restart();
                foreach (Ad ad in ads)
                {
                    var titleParts = nlpService.Lemmatize(ad.Title);
                    var descParts = nlpService.Lemmatize(ad.Description);
                }
                watch.Stop();
                WriteLine($"Elapsed time for lemmatization: {watch.Elapsed.TotalMinutes} minutes.");
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
