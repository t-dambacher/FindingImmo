﻿using System;
using System.Collections.Generic;
using System.Linq;
using FindingImmo.Core.Domain.DataAccess;
using FindingImmo.Core.Domain.Models;
using FindingImmo.Core.Scraping.DataTransfer;
using OpenQA.Selenium;

namespace FindingImmo.Core.Scraping.Sites.Shorp
{
    sealed internal class ShorpScrapper : AdReferencesScraper
    {
        public ShorpScrapper(IAdRepository repository) 
            : base(repository, Website.Shorp)
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
