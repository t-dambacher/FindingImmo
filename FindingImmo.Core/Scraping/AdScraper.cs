using FindingImmo.Core.Domain.Models;
using FindingImmo.Core.Scraping.DataTransfer;
using OpenQA.Selenium;

namespace FindingImmo.Core.Scraping
{
    internal abstract class AdScraper
    {
        public abstract Ad Scrap(IWebDriver driver, AdReference reference);
    }
}
