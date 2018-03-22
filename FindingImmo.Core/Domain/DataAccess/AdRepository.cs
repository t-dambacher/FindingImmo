﻿using FindingImmo.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FindingImmo.Core.Domain.DataAccess
{
    sealed internal class AdRepository : IAdRepository
    {
        private readonly ImmoDbContext _dbContext;

        public AdRepository(ImmoDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void SaveIfNotExist(IEnumerable<Ad> ads)
        {
            if (ads == null)
                throw new ArgumentNullException(nameof(ads));

            int i = 0;
            foreach (Ad newAd in ads)
            {
                if (!DoesExternalIdExists(newAd.Origin, newAd.ExternalId))
                {
                    _dbContext.Set<Ad>().Add(newAd);

                    if (i % 100 == 0)
                        _dbContext.SaveChanges();
                }

                ++i;
            }

            _dbContext.SaveChanges();
        }

        public bool DoesExternalIdExists(Website website, string externalId)
        {
            if (string.IsNullOrWhiteSpace(externalId))
                throw new ArgumentNullException(nameof(externalId));

            return _dbContext.Set<Ad>().Any(a => a.Origin == website && a.ExternalId == externalId);
        }

        public void MarkAsSent(Ad ad)
        {
            if (ad == null)
                throw new ArgumentNullException(nameof(ad));

            ad.State = State.Sent;
            _dbContext.Entry(ad).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
