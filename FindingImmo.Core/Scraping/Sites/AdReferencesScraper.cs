using FindingImmo.Core.Domain.DataAccess;
using FindingImmo.Core.Domain.Models;
using FindingImmo.Core.Scraping.DataTransfer;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace FindingImmo.Core.Scraping.Sites
{
    internal abstract class AdReferencesScraper
    {
        public Website Website { get; }
        public abstract string RootUrl { get; }

        private readonly IAdRepository _repository;

        protected AdReferencesScraper(IAdRepository repository, Website site)
        {
            this._repository = repository;
            this.Website = site;
        }

        public IEnumerable<AdReference> Scrap(IWebDriver driver)
        {
            bool keepScraping;

            LaunchSearch(driver);

            do
            {
                keepScraping = false;

                IEnumerable<AdReference> adReferences = GetSearchResultsFromCurrentPage(driver) ?? Enumerable.Empty<AdReference>();
                string currentUrl = driver.Url;

                if (!adReferences.All(AlreadyExists))
                {
                    foreach (AdReference adReference in adReferences)
                        yield return adReference;

                    if (driver.Url != currentUrl)   // As deffered execution might have change the url of the current page... we should reset the context to it previous state
                        driver.Navigate().GoToUrl(currentUrl);

                    keepScraping = MoveToNextResultPage(driver);
                }
            }
            while (keepScraping);
        }

        private bool AlreadyExists(AdReference reference)
        {
            return reference != null && this._repository.DoesExternalIdExists(this.Website, reference.Reference);
        }

        protected virtual void LaunchSearch(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(RootUrl);
        }

        protected abstract IEnumerable<AdReference> GetSearchResultsFromCurrentPage(IWebDriver driver);
        protected abstract bool MoveToNextResultPage(IWebDriver driver);
    }
}
