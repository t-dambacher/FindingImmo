using System;
using System.Collections.Generic;
using System.Linq;
using FindingImmo.Core.Domain.DataAccess;
using FindingImmo.Core.Domain.Models;
using FindingImmo.Core.Scraping.DataTransfer;
using OpenQA.Selenium;

namespace FindingImmo.Core.Scraping.Sites.OrpiBrumath
{
    sealed internal class OrpiBrumathScrapper : AdReferencesScraper
    {
        private const string HomeUrl = "https://www.orpi.com";

        public override string RootUrl => HomeUrl + "/recherche/buy?realEstateTypes%5B%5D=maison&locations%5B%5D=bas-rhin&sort=date-down&layoutType=mixte&minPrice=100000&maxPrice=350000&minSurface=100&maxSurface=180";

        public OrpiBrumathScrapper(IAdRepository repository)
            : base(repository, Website.OrpiBrumath)
        {
        }

        protected override IEnumerable<AdReference> GetSearchResultsFromCurrentPage(IWebDriver driver)
        {
            var euNotif = driver.FindElements(By.ClassName("cc-compliance"));
            if (euNotif.Any())
                euNotif.First().FindElement(By.TagName("a")).Click();

            return driver.FindElement(By.ClassName("resultLayout-estateList"))
                .FindElements(By.TagName("li"))
                .Select(li =>
                {
                    return new AdReference(li.GetAttribute("id"), li.FindElement(By.TagName("a")).GetAttribute("href"))
                    {
                        Description = li.FindElement(By.ClassName("estateItem-head")).Text,
                        PictureUrl = li.FindElement(By.ClassName("estateItem-media")).FindElement(By.TagName("img")).GetAttribute("src")
                    };
                }
            )
            .ToList();
        }

        protected override bool MoveToNextResultPage(IWebDriver driver)
        {
            return false;
        }
    }
}
