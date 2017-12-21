using FindingImmo.Core.Scraping.DataTransfer;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace FindingImmo.Core.Scraping
{
    internal abstract class AdReferencesScraper
    {
        protected AdReferencesScraper()
        { }

        public IEnumerable<AdReference> Scrap(IWebDriver driver)
        {
            List<AdReference> res = new List<AdReference>();
            bool keepScraping;

            LaunchSearch(driver);

            do
            {
                keepScraping = false;

                IEnumerable<AdReference> adReferences = GetSearchResultsFromCurrentPage(driver);
                if (adReferences != null && adReferences.Any())
                {
                    res.AddRange(adReferences);
                    keepScraping = MoveToNextResultPage(driver);
                }
            }
            while (keepScraping);

            return res;
        }

        protected abstract void LaunchSearch(IWebDriver driver);
        protected abstract IEnumerable<AdReference> GetSearchResultsFromCurrentPage(IWebDriver driver);
        protected abstract bool MoveToNextResultPage(IWebDriver driver);
    }
}
