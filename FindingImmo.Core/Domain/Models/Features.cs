namespace FindingImmo.Core.Domain.Models
{
    public class Features
    {
        public long AdId { get; set; }
        public virtual Ad Ad { get; set; }

        public BooleanFeatureResult TwoFamilyHouse { get; set; }

        public Features()
        { }

        public Features(long adId)
        {
            this.AdId = adId;
        }
    }
}
