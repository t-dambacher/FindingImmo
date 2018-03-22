using FindingImmo.Core.Domain.Models;
using System.Collections.Generic;

namespace FindingImmo.Core.Domain.DataAccess
{
    public interface IAdRepository
    {
        void SaveIfNotExist(IEnumerable<Ad> ads);

        bool DoesExternalIdExists(Website website, string externalId);

        void MarkAsSent(Ad ad);
    }
}
