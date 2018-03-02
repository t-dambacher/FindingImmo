using System;

namespace FindingImmo.Core.Scraping.DataTransfer
{
    public sealed class AdReference
    {
        public string Reference { get; }
        public string Url { get; }

        public AdReference(string reference, string url)
        {
            if (reference == null)
                throw new ArgumentNullException(nameof(reference));

            if (url == null)
                throw new ArgumentNullException(nameof(url));

            this.Reference = reference;
            this.Url = url;
        }

        public override string ToString()
        {
            return this.Reference;
        }
    }
}
