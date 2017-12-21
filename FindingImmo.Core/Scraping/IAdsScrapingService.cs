using System.Collections.Generic;
using FindingImmo.Core.Domain.Models;

namespace FindingImmo.Core.Scraping
{
    public interface IAdsScrapingService
    {
        IEnumerable<Ad> ScrapAll();

        void UpdateAll();
    }
}
