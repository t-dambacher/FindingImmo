using FindingImmo.Core.Scraping.DataTransfer;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Linq;
using System.Collections.Generic;

namespace FindingImmo.Core.Scraping
{
    internal abstract class WebPagesScraper
    {
        public AdScraper AdScraper { get; }

        public abstract IEnumerable<WebPage> Scrap();

        protected WebPagesScraper(AdScraper scraper)
        {
            this.AdScraper = scraper ?? throw new ArgumentNullException(nameof(scraper));
        }

        protected IWebDriver GetWebDriver()
        {
            return new FirefoxDriver(
                new FirefoxOptions() {
                    LogLevel = FirefoxDriverLogLevel.Error,
                    PageLoadStrategy = PageLoadStrategy.Eager
                }
            );
        }
    }
}
