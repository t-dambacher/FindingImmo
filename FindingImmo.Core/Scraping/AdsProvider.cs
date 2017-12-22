using FindingImmo.Core.Domain.Models;
using FindingImmo.Core.Infrastructure;
using FindingImmo.Core.Scraping.DataTransfer;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace FindingImmo.Core.Scraping
{
    internal abstract class AdsProvider
    {
        private readonly AdReferencesScraper _referencesScraper;
        private readonly AdScraper _adScraper;
        private readonly ILogger _logger;

        public AdsProvider(AdReferencesScraper referencesScraper, AdScraper adScraper, ILogger logger)
        {
            this._referencesScraper = referencesScraper;
            this._adScraper = adScraper;
            this._logger = logger;
        }

        public IEnumerable<Ad> Provide()
        {
            using (IWebDriver driver = new WebDriver(this._logger))
            {
                foreach (AdReference reference in this._referencesScraper.Scrap(driver))
                {
                    Ad ad = this._adScraper.Scrap(driver, reference);
                    if (ad == null)
                    {
                        this._logger.Error($"No ad found for reference {reference}");
                        continue;
                    }

                    yield return ad;
                }
            }
        }
    }
}
