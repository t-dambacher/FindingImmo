using System.Collections.Generic;

namespace FindingImmo.Core.Scraping
{
    public interface IScrapingService
    {
        IEnumerable<DataTransfer.Ad> ScrapAll();
    }
}
