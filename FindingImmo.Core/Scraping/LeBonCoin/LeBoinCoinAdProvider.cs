﻿using FindingImmo.Core.Infrastructure;

namespace FindingImmo.Core.Scraping.LeBonCoin
{
    internal sealed class LeBoinCoinAdProvider : AdsProvider
    {
        public LeBoinCoinAdProvider(LeBonCoinAdReferencesScraper referenceScraper, LeBonCoinAdScraper adScraper, ILogger logger)
            : base(referenceScraper, adScraper, logger)
        { }
    }
}
