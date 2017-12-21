using FindingImmo.Core.Infrastructure;
using FindingImmo.Core.Scraping;
using FindingImmo.Core.Scraping.LeBonCoin;
using System;
using static System.Console;

namespace FindingImmo.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ILogger logger = Logger.Instance;

            try
            {
                IAdsScrapingService scrapingService = GetAdsScrapingService(logger);
                scrapingService.UpdateAll();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            ReadKey();
        }

        private static IAdsScrapingService GetAdsScrapingService(ILogger logger)
        {
            return new AdsScrapingService(
                new AdsProvider[]
                {
                    new LeBoinCoinAdProvider(
                        new LeBonCoinAdReferencesScraper(logger), new LeBonCoinAdScraper(logger), logger
                    )
                },
                logger
            );
        }
    }
}
