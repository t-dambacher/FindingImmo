using FindingImmo.Core.Domain.DataAccess;
using FindingImmo.Core.Domain.Models;
using FindingImmo.Core.Infrastructure.Logging;
using FindingImmo.Core.Scraping.Sites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FindingImmo.Core.Services
{
    internal sealed class AdsScrapingService
    {
        private readonly IEnumerable<AdReferencesScraper> _scrapers;
        private readonly ILogger _logger;
        private readonly IAdRepository _repository;

        public AdsScrapingService(IEnumerable<AdReferencesScraper> scrapers, IAdRepository repository, ILogger logger)
        {
            this._scrapers = scrapers;
            this._logger = logger;
            this._repository = repository;
        }

        public IEnumerable<Ad> UpdateAll()
        {
            IEnumerable<Ad> all = ScrapAll();
            this._repository.SaveIfNotExist(all);
            return all.Where(a => a.State != State.Sent).ToList();
        }

        public IEnumerable<Ad> ScrapAll()
        {
            // todo: can be optimised with a Parallel.ForEach, when everything else will be done
            return this._scrapers.SelectMany(Scrap).ToList();
        }

        private IEnumerable<Ad> Scrap(AdReferencesScraper scraper)
        {
            using (var driver = new WebDriver(this._logger))
            {
                try
                {
                    return scraper.Scrap(driver).Select(r => new Ad(r, scraper.Website)).ToList();
                }
                catch (NotImplementedException)
                { }

                return Enumerable.Empty<Ad>();
            }
        }
    }
}
