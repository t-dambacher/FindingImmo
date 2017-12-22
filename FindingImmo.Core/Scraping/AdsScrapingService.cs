﻿using FindingImmo.Core.Domain.DataAccess;
using FindingImmo.Core.Domain.Models;
using FindingImmo.Core.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace FindingImmo.Core.Scraping
{
    internal sealed class AdsScrapingService : IAdsScrapingService
    {
        private readonly IEnumerable<AdsProvider> _providers;
        private readonly ILogger _logger;
        private readonly IAdRepository _repository;

        public AdsScrapingService(IEnumerable<AdsProvider> providers, IAdRepository repository, ILogger logger)
        {
            this._providers = providers;
            this._logger = logger;
            this._repository = repository;
        }

        public void UpdateAll()
        {
            this._repository.SaveIfNotExist(ScrapAll());
        }

        public IEnumerable<Ad> ScrapAll()
        {
            return this._providers.SelectMany(p => p.Provide());
        }
    }
}
