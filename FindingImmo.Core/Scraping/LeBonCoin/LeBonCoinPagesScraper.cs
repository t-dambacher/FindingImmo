using FindingImmo.Core.Infrastructure;
using FindingImmo.Core.Scraping.DataTransfer;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingImmo.Core.Scraping.LeBonCoin
{
    internal sealed class LeBonCoinPagesScraper : WebPagesScraper
    {
        private readonly ILogger _logger;
        private readonly LeBonCoinConfiguration _configuration;

        public LeBonCoinPagesScraper(ILogger logger)
            : base(new LeBonCoinAdScraper(logger))
        {
            this._logger = logger;
            this._configuration = new LeBonCoinConfiguration();
        }

        public override IEnumerable<WebPage> Scrap()
        {
            IList<WebPage> res = new List<WebPage>();
            int pageIndex = 0;

            using (IWebDriver driver = GetWebDriver())
            {
                PrepareForSearching(driver);
                bool keepScraping = false;

                do
                {
                    keepScraping = false;

                    IEnumerable<AdReference> adReferences = GetSearchResultsFromCurrentPage(driver);
                    if (adReferences != null && adReferences.Any())
                    {
                        res.Add(new WebPage(pageIndex + 1, adReferences));
                        keepScraping = MoveToNextPage(driver);
                    }
                }
                while (keepScraping);
            }

            return res;
        }

        private void PrepareForSearching(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(this._configuration.RootUrl);

            // Surface
            SetComboValue(driver, "sqs", _configuration.MinSurface.ToString());
            SetComboValue(driver, "sqe", _configuration.MaxSurface.ToString());

            // Prix
            SetComboValue(driver, "ps", _configuration.MinPrice.ToString());
            SetComboValue(driver, "pe", _configuration.MaxPrice.ToString());

            // Nombre de pièces
            SetComboValue(driver, "rooms_ros", _configuration.MinRooms.ToString());
            SetComboValue(driver, "rooms_roe", _configuration.MaxRooms.ToString());

            // Type "maison" (et non appartement, terrain, etc.)
            driver.FindElement(By.Id("ret_1")).Click();

            driver.FindElement(By.Id("searchbutton")).Click();
        }

        private AdReference GetSearchResult(IWebElement li)
        {
            string url = li.FindElement(By.TagName("a")).GetAttribute("href");
            string id = System.IO.Path.GetFileNameWithoutExtension(new Uri(url).LocalPath);
            return new AdReference(id, url);
        }

        private IEnumerable<AdReference> GetSearchResultsFromCurrentPage(IWebDriver driver)
        {
            try
            {
                IEnumerable<IWebElement> lis = driver.FindElement(By.Id("listingAds")).FindElement(By.TagName("section")).FindElement(By.TagName("section")).FindElements(By.TagName("li"));
                return lis.Select(GetSearchResult).ToList();
            }
            catch (Exception ex)
            {
                this._logger.Error("Error while accessing the search result of the current page", ex);
                return Enumerable.Empty<AdReference>();
            }
        }

        private void SetComboValue(IWebDriver driver, string comboId, string value)
        {
            new SelectElement(driver.FindElement(By.Id(comboId))).SelectByText(value);
        }

        private bool MoveToNextPage(IWebDriver driver)
        {
            IWebElement pager = driver.FindElement(By.ClassName("pagination_links_container"));
            IWebElement currentPageSpan = pager.FindElements(By.TagName("span")).SingleOrDefault(s => s.GetAttribute("class")?.Contains("selected") ?? false);

            int currentPage = int.Parse(currentPageSpan.Text);
            if (currentPage > this._configuration.LastPageToCheck)
                return false;

            IWebElement nextPageLink = pager.FindElements(By.TagName("a")).Single(a => a.Text == (currentPage + 1).ToString());
            nextPageLink.Click();

            return true;
        }
    }
}
