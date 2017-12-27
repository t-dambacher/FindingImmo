using FindingImmo.Core.Domain.Models;
using System;
using System.Linq;

namespace FindingImmo.Core.Domain.DataAccess
{
    internal sealed class FeaturesRepository : IFeaturesRepository
    {
        private readonly ImmoDbContext _dbContext;

        public FeaturesRepository(ImmoDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public bool DoesFeatureExistsForAd(long adId)
        {
            return this._dbContext.Set<Features>().Any(f => f.AdId == adId);
        }

        public void Save(Features features)
        {
            if (features == null)
                throw new ArgumentNullException(nameof(features));

            features.AdId = features.Ad?.Id ?? features.AdId;
            if (features.AdId == default(long))
                throw new ArgumentException("No ad id was specified.", nameof(features.AdId));

            if (DoesFeatureExistsForAd(features.AdId))
                throw new ArgumentException("A feature already exists for this ad, consider updating it.", nameof(features.AdId)); // todo: one day

            this._dbContext.Set<Features>().Add(features);
            this._dbContext.SaveChanges();
        }
    }
}
