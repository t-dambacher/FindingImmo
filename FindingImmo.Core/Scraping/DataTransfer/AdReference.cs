using System;

namespace FindingImmo.Core.Scraping.DataTransfer
{
    public sealed class AdReference
    {
        public string Reference { get; }
        public string Url { get; }

        public AdReference(string reference, string url)
        {
            this.Reference = reference ?? throw new ArgumentNullException(nameof(reference));
            this.Url = url ?? throw new ArgumentNullException(nameof(url));
        }
    }
}
