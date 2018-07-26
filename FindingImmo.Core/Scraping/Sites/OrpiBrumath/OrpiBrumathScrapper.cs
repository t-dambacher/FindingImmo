using System;
using System.Collections.Generic;
using System.Linq;
using FindingImmo.Core.Domain.DataAccess;
using FindingImmo.Core.Domain.Models;
using FindingImmo.Core.Scraping.DataTransfer;
using OpenQA.Selenium;

namespace FindingImmo.Core.Scraping.Sites.OrpiBrumath
{
    internal sealed class OrpiBrumathScrapper : AdReferencesScraper
    {
        private const string HomeUrl = "https://www.orpi.com";

        public override string RootUrl => HomeUrl + "/recherche/buy?realEstateTypes%5B%5D=maison&locations%5B%5D=bas-rhin&sort=date-down&layoutType=mixte&minPrice=100000&maxPrice=350000&minSurface=100&maxSurface=180";

        public OrpiBrumathScrapper(IAdRepository repository)
            : base(repository, Website.OrpiBrumath)
        {
        }

        private void RemoveEuNotif(IWebDriver driver)
        {
            IEnumerable<IWebElement> euNotif = driver.FindElements(By.ClassName("cc-compliance"));
            if (euNotif.Any() && euNotif.First().Displayed)
                euNotif.First().FindElement(By.TagName("a")).Click();
        }

        protected override IEnumerable<AdReference> GetSearchResultsFromCurrentPage(IWebDriver driver)
        {
            RemoveEuNotif(driver);

            string currentUrl = driver.Url;
            try
            {
                IList<AdReference> references = new List<AdReference>();
                foreach (IWebElement li in driver.FindElement(By.ClassName("resultLayout-estateList")).FindElements(By.TagName("li")))
                {
                    references.Add(
                        new AdReference(li.GetAttribute("id"), li.FindElement(By.TagName("a")).GetAttribute("href"))
                        {
                            Description = li.FindElement(By.ClassName("estateItem-head")).Text,
                            PictureUrl = li.FindElement(By.ClassName("estateItem-media")).FindElement(By.TagName("img")).GetAttribute("src")
                        }
                    );
                }

                return references.Where(ad => SizeIsAppropriate(ad, driver)).ToList();
            }
            finally
            {
                driver.Url = currentUrl;
            }
        }

        private bool SizeIsAppropriate(AdReference ad, IWebDriver driver)
        {
            driver.Navigate().GoToUrl(ad.DetailUrl);
            RemoveEuNotif(driver);
            int terrain = GetTailleTerrain(driver);
            return terrain == 0 || terrain > 500 /* m2 */ || terrain < 10 && terrain > 5; // 5 ha
        }

        private int GetTailleTerrain(IWebDriver driver)
        {
            IWebElement res = driver.FindElement(By.Id("detail"))
               .FindElements(By.TagName("li"))
               .FirstOrDefault(li => li.FindElements(By.TagName("mark")).Any(m => m.Text == "Surface du terrain"));
            string sizeAsString = res?.FindElements(By.TagName("mark")).FirstOrDefault(m => m.Text != "Surface du terrain")?.Text;
            try
            {
                if (sizeAsString == null)
                    return 0;
                return int.Parse(new string(sizeAsString.Where(c => char.IsNumber(c) && c != '²').ToArray()));
            }
            catch { return 0; }
        }

        protected override bool MoveToNextResultPage(IWebDriver driver)
        {
            RemoveEuNotif(driver);

            IWebElement navBar = driver.FindElements(By.TagName("nav")).FirstOrDefault(nav => nav.GetAttribute("class")?.Contains("paging") ?? false);
            if (navBar == null)
                return false;

            IEnumerable<IWebElement> items = navBar.FindElements(By.ClassName("paging-item "))
                .OrderBy(item => (int.TryParse(item.Text, out int p) ? 1 : -1) * p)
                .ToList();

            if (items.LastOrDefault()?.GetAttribute("class")?.Contains("current") ?? false)
                return false;

            navBar.FindElements(By.ClassName("paging-navButton")).Last().Click();
            return true;
        }
    }
}
