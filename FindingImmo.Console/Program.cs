using FindingImmo.Core.Infrastructure;
using FindingImmo.Core.Scraping;
using FindingImmo.Core.Scraping.LeBonCoin;
using System;
using System.Linq;
using System.Diagnostics;

namespace FindingImmo.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ILogger logger = Logger.Instance;

            try
            {
                IScrapingService scrapingService = new ScrapingService(new WebPagesScraper[] { new LeBonCoinPagesScraper(logger) }, logger);

                var watch = Stopwatch.StartNew();
                var result = scrapingService.ScrapAll();
                watch.Stop();

                logger.Info($"{result.Count()} results scraped in {watch.Elapsed}");
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            System.Console.ReadKey();
        }
    }
}
