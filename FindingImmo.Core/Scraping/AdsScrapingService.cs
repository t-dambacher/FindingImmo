using FindingImmo.Core.Domain.Models;
using FindingImmo.Core.Infrastructure;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace FindingImmo.Core.Scraping
{
    internal sealed class AdsScrapingService : IAdsScrapingService
    {
        private readonly IEnumerable<AdsProvider> _providers;
        private readonly ILogger _logger;

        public AdsScrapingService(IEnumerable<AdsProvider> providers, ILogger logger)
        {
            this._providers = providers;
            this._logger = logger;
        }

        public void UpdateAll()
        {
#if DEBUG
            Stopwatch watch = Stopwatch.StartNew();
#endif      
            IEnumerable<Ad> webAds = ScrapAll();

#if DEBUG
            watch.Stop();
            this._logger.Info($"{webAds.Count()} results scraped in {watch.Elapsed}");
#endif
        }

        public IEnumerable<Ad> ScrapAll()
        {
            return this._providers.SelectMany(p => p.Provide()).ToList();
        }
    }
}
