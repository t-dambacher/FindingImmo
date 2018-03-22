﻿using System;
using System.Collections.Generic;
using System.Linq;
using FindingImmo.Core.Domain.DataAccess;
using FindingImmo.Core.Domain.Models;
using FindingImmo.Core.Scraping.DataTransfer;
using OpenQA.Selenium;

namespace FindingImmo.Core.Scraping.Sites.ImmobiliereDeHanau
{
    sealed internal class ImmobiliereDeHanauScrapper : AdReferencesScraper
    {
        public override string RootUrl => throw new NotImplementedException();

        public ImmobiliereDeHanauScrapper(IAdRepository repository) 
            : base(repository, Website.ImmobiliereDeHanau)
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
