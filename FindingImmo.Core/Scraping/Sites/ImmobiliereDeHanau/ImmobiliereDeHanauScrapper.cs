﻿using System;
using System.Collections.Generic;
using System.Linq;
using FindingImmo.Core.Domain.DataAccess;
using FindingImmo.Core.Domain.Models;
using FindingImmo.Core.Scraping.DataTransfer;
using OpenQA.Selenium;

namespace FindingImmo.Core.Scraping.Sites.ImmobiliereDesRohan
{
    internal sealed class ImmobiliereDesRohanScrapper : AdReferencesScraper
    {
        public override string RootUrl => throw new NotImplementedException();

        public ImmobiliereDesRohanScrapper(IAdRepository repository) 
            : base(repository, Website.ImmobiliereDesRohan)
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
