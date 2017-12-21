using FindingImmo.Core.Infrastructure;
using FindingImmo.Core.Scraping.DataTransfer;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FindingImmo.Core.Scraping.LeBonCoin
{
    internal sealed class LeBonCoinAdReferencesScraper : AdReferencesScraper
    {
        private readonly ILogger _logger;
        private readonly LeBonCoinConfiguration _configuration;

        public LeBonCoinAdReferencesScraper(ILogger logger)
            : base()
        {
            this._logger = logger;
            this._configuration = new LeBonCoinConfiguration();
        }

        protected override void LaunchSearch(IWebDriver driver)
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

        protected override bool MoveToNextResultPage(IWebDriver driver)
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

        protected override IEnumerable<AdReference> GetSearchResultsFromCurrentPage(IWebDriver driver)
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

        private AdReference GetSearchResult(IWebElement li)
        {
            string url = li.FindElement(By.TagName("a")).GetAttribute("href");
            string id = System.IO.Path.GetFileNameWithoutExtension(new Uri(url).LocalPath);
            return new AdReference(id, url);
        }
    }
}
