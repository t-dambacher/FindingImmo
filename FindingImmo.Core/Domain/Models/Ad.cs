using System;

namespace FindingImmo.Core.Domain.Models
{
    public class Ad
    {
        public long Id { get; set; }
        public string ExternalId { get; set; }
        public DateTime Creation { get; set; }
        public Website Origin { get; set; }

        public int Price { get; set; }
        public string PostalCode { get; set; }
        public bool IsPro { get; set; }
        public int RoomsCount { get; set; }
        public int Surface { get; set; }
        public GES GES { get; set; }
        public EnergyClass EnergyClass { get; set; }
        public string Description { get; set; }

        public Ad()
        { }

        public Ad(Website origin, string externalId)
        {
            if (!Enum.IsDefined(typeof(Website), origin))
                throw new ArgumentException(nameof(origin));

            if (string.IsNullOrWhiteSpace(externalId))
                throw new ArgumentNullException(nameof(externalId));

            this.ExternalId = externalId;
            this.Origin = origin;
            this.Creation = DateTime.Now;
        }
    }
}
