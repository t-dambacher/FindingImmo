using System;
using System.Collections.Generic;

namespace FindingImmo.Core.Scraping.DataTransfer
{
    public sealed class SearchResultPage
    {
        public int Number { get; }
        public IEnumerable<AdReference> Ads { get; }

        public SearchResultPage(int pageNumber, IEnumerable<AdReference> ads)
        {
            if (ads == null)
                throw new ArgumentNullException(nameof(ads));

            this.Number = pageNumber;
            this.Ads = ads;
        }
    }
}
