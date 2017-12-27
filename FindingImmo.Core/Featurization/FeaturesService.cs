using FindingImmo.Core.Domain.DataAccess;
using FindingImmo.Core.Domain.Models;
using FindingImmo.Core.Featurization.Evaluators;
using System;

namespace FindingImmo.Core.Featurization
{
    internal sealed class FeaturesService : IFeaturesService
    {
        private readonly IFeaturesRepository _repository;
        private readonly TwoFamilyFeatureEvaluator _twoFamilyFeature;

        public FeaturesService(IFeaturesRepository repository, TwoFamilyFeatureEvaluator twoFamilyFeature)
        {
            this._repository = repository;
            this._twoFamilyFeature = twoFamilyFeature;
        }

        public Features Determine(Ad ad)
        {
            if (ad == null)
                throw new ArgumentNullException(nameof(ad));

            return new Features(ad.Id)
            {
                TwoFamilyHouse = this._twoFamilyFeature.Evaluate(ad)
            };
        }

        public void DetermineAndSave(Ad ad)
        {
            if (ad == null)
                throw new ArgumentNullException(nameof(ad));

            if (this._repository.DoesFeatureExistsForAd(ad.Id))
                throw new ArgumentException("The ad already has a list of features that has been evaluated and saved.", nameof(ad));    // todo: handle updates & dbmigrations & etc.

            Features features = Determine(ad);
            if (features == null)
                throw new InvalidOperationException("An error occured while determining the list of features of the ad, as it returned null from its determination.");

            this._repository.Save(features);
        }
    }
}
