using FindingImmo.Core.Scraping.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindingImmo.Core.Scraping
{
    internal abstract class AdScraper
    {
        public abstract Ad Scrap(AdReference reference);
    }
}
