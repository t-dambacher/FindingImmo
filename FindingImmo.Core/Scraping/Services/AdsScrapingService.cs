﻿using FindingImmo.Core.Domain.DataAccess;
using FindingImmo.Core.Domain.Models;
using FindingImmo.Core.Infrastructure.Logging;
using FindingImmo.Core.Scraping.Sites;
using System.Collections.Generic;
using System.Linq;

namespace FindingImmo.Core.Scraping.Services
{
    internal sealed class AdsScrapingService : IAdsScrapingService
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
            return all;
        }

        public IEnumerable<Ad> ScrapAll()
        {
            using (var driver = new WebDriver(this._logger))
            {
                return this._scrapers.SelectMany(p => p.Scrap(driver).Select(r => new Ad(r, p.Website)));
            }
        }
    }
}
