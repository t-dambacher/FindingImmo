using System;
using System.Collections.Generic;

namespace FindingImmo.Core.Domain.Models
{
    public class Ad : IEntity
    {
        public long Id { get; set; }
        public string ExternalId { get; set; }
        public DateTime Creation { get; set; }
        public DateTime LastScraping { get; set; }
        public Website Origin { get; set; }
        public int Price { get; set; }
        public string PostalCode { get; set; }
        public bool IsPro { get; set; }
        public int RoomsCount { get; set; }
        public int Surface { get; set; }
        public GES GES { get; set; }
        public EnergyClass EnergyClass { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual Features Features { get; set; }

        public Ad()
        {
            this.Pictures = new List<Picture>();
        }

        public Ad(Website origin, string externalId)
             : this()
        {
            if (!Enum.IsDefined(typeof(Website), origin))
                throw new ArgumentException(nameof(origin));

            if (string.IsNullOrWhiteSpace(externalId))
                throw new ArgumentNullException(nameof(externalId));

            this.ExternalId = externalId;
            this.Origin = origin;
            this.LastScraping = this.Creation = DateTime.Now;
        }
    }
}
