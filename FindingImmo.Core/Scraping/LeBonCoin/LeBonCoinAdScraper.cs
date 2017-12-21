using System;
using System.Collections.Generic;
using FindingImmo.Core.Infrastructure;
using FindingImmo.Core.Scraping.DataTransfer;

namespace FindingImmo.Core.Scraping.LeBonCoin
{
    sealed internal class LeBonCoinAdScraper : AdScraper
    {
        private readonly ILogger _logger;

        public LeBonCoinAdScraper(ILogger logger)
        {
            this._logger = logger;
        }

        public override Ad Scrap(AdReference reference)
        {
            //            throw new NotImplementedException();
            return new Ad();
        }
    }
}
