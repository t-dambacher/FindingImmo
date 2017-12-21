using FindingImmo.Core.Infrastructure;
using System;
using System.Collections.Generic;

namespace FindingImmo.Core.Scraping
{
    internal sealed class ScrapingService : IScrapingService
    {
        private readonly IEnumerable<WebPagesScraper> _pagesScrapers;
        private readonly ILogger _logger;

        public ScrapingService(IEnumerable<WebPagesScraper> pagesScrapers, ILogger logger)
        {
            this._pagesScrapers = pagesScrapers;
            this._logger = logger;
        }

        public IEnumerable<DataTransfer.Ad> ScrapAll()
        {
            IList<DataTransfer.Ad> ads = new List<DataTransfer.Ad>();

            foreach (WebPagesScraper scraper in this._pagesScrapers)
            foreach (DataTransfer.WebPage page in scraper.Scrap())
            foreach (DataTransfer.AdReference adReference in page.Ads)
            {
                DataTransfer.Ad ad = scraper.AdScraper.Scrap(adReference);
                if (ad == null)
                    this._logger.Error($"No ad found for reference {adReference}");

                ads.Add(ad);
            }

            return ads;
        }
    }
}
