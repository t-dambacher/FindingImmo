using System;

namespace FindingImmo.Core.Scraping.DataTransfer
{
    public sealed class AdReference
    {
        public string Reference { get; }
        public string DetailUrl { get; }
        public string PictureUrl { get; set; }
        public string Description { get; set; }

        public AdReference(string reference, string detailUrl)
        {
            if (reference == null)
                throw new ArgumentNullException(nameof(reference));

            if (detailUrl == null)
                throw new ArgumentNullException(nameof(detailUrl));

            this.Reference = reference;
            this.DetailUrl = detailUrl;
        }

        public override string ToString()
        {
            return this.Reference;
        }
    }
}
