using FindingImmo.Core.Domain.Models;

namespace FindingImmo.Core.Domain.DataAccess
{
    public interface IFeaturesRepository
    {
        bool DoesFeatureExistsForAd(long adId);
        void Save(Features feature);
    }
}
