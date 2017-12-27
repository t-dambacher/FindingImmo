using FindingImmo.Core.Domain.Models;

namespace FindingImmo.Core.Featurization
{
    public interface IFeaturesService
    {
        Features Determine(Ad ad);
        void DetermineAndSave(Ad ad);
    }
}
