using System;
using System.Collections.Generic;
using FindingImmo.Core.Scraping.DataTransfer;

namespace FindingImmo.Core.Domain.Models
{
    public class Ad
    {
        public long Id { get; set; }
        public string ExternalId { get; set; }
        public string Description { get; set; }
        public DateTime Creation { get; set; }
        public Website Origin { get; set; }
        public string PictureUrl { get; set; }
        public string DetailUrl { get; set; }
        public State State { get; set; }

        public Ad()
        {
            this.Creation = DateTime.Now;
            this.State = State.Unknown;
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
        }

        public Ad(AdReference model, Website origin)
            : this()
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            this.Description = model.Description;
            this.DetailUrl = model.DetailUrl;
            this.ExternalId = model.Reference;
            this.Origin = origin;
            this.PictureUrl = model.PictureUrl;
        }
    }
}
