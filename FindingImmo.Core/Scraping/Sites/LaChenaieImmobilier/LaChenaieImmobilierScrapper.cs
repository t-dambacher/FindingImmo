using System;
using System.Collections.Generic;
using System.Linq;
using FindingImmo.Core.Domain.DataAccess;
using FindingImmo.Core.Domain.Models;
using FindingImmo.Core.Scraping.DataTransfer;
using OpenQA.Selenium;

namespace FindingImmo.Core.Scraping.Sites.LaChenaieImmobilier
{
    internal sealed class LaChenaieImmobilierScrapper : AdReferencesScraper
    {
        private const string HomeUrl = "http://www.lachenaieimmobilier.fr";
        public override string RootUrl => HomeUrl + "/?search-class=DB_CustomSearch_Widget-db_customsearch_widget&widget_number=5&all-4=VENTE&cs-all-0=&cs--1=maison&cs-prix_vente-2=&cs-surface_habitable-3=&search=Rechercher";

        public LaChenaieImmobilierScrapper(IAdRepository repository) 
            : base(repository, Website.LaChenaieImmobilier)
        {
        }

        protected override IEnumerable<AdReference> GetSearchResultsFromCurrentPage(IWebDriver driver)
        {
            List<AdReference> results = driver.FindElement(By.TagName("main"))
                .FindElements(By.TagName("article"))
                .Select(b =>
                {
                    var link = b.FindElements(By.TagName("a")).FirstOrDefault(e => e.GetAttribute("class")?.Contains("thumbnail") ?? false);
                    string reference = link?.GetAttribute("href")?.Replace(HomeUrl, string.Empty)?.Replace("/", string.Empty) ?? string.Empty;
                    
                    return new AdReference(reference, HomeUrl + "/" + reference)
                    {
                        PictureUrl = b.FindElement(By.TagName("img")).GetAttribute("src"),
                        Description = b.FindElement(By.ClassName("entry-title"))?.Text
                    };
                })
                .ToList();

            // todo: should filter on content
            return results;
        }

        protected override bool MoveToNextResultPage(IWebDriver driver)
        {
            var nextButton = driver.FindElement(By.TagName("main"))
                .FindElements(By.TagName("ul")).FirstOrDefault(u => u.GetAttribute("class")?.Contains("pagination") ?? false)
                .FindElements(By.TagName("a")).FirstOrDefault(a => a.GetAttribute("class")?.Contains("next") ?? false);
            nextButton?.Click();
            return nextButton != null;
        }
    }
}
