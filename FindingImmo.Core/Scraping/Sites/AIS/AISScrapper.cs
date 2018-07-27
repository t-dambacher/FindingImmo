using FindingImmo.Core.Domain.DataAccess;
using FindingImmo.Core.Domain.Models;
using FindingImmo.Core.Scraping.DataTransfer;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace FindingImmo.Core.Scraping.Sites.AIS
{
    internal sealed class AisScrapper : AdReferencesScraper
    {
        public override string RootUrl => throw new NotImplementedException();

        public AisScrapper(IAdRepository repository) 
            : base(repository, Website.AIS)
        {
        }

        protected override IEnumerable<AdReference> GetSearchResultsFromCurrentPage(IWebDriver driver)
        {
            throw new NotImplementedException();
        }

        protected override void LaunchSearch(IWebDriver driver)
        {
            throw new NotImplementedException();
        }

        protected override bool MoveToNextResultPage(IWebDriver driver)
        {
            throw new NotImplementedException();
        }
    }
}
