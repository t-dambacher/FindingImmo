using System.Collections.Generic;
using FindingImmo.Core.Domain.Models;

namespace FindingImmo.Core.Scraping.Services
{
    public interface IAdsScrapingService
    {
        IEnumerable<Ad> ScrapAll();

        IEnumerable<Ad> UpdateAll();
    }
}
