using System;

namespace FindingImmo.Core.Domain.Models
{
    public class Picture : IEntity
    {
        public long Id { get; set; }
        public long AdId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Url { get; set; }
        public virtual Ad Ad { get; set; }

        public Picture()
        { }

        public Picture(Ad ad, string url, int width, int height)
        {
            if (ad == null)
                throw new ArgumentNullException(nameof(ad));

            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            this.Url = url;
            this.Width = width;
            this.Height = height;
            this.Ad = ad;
        }
    }
}
