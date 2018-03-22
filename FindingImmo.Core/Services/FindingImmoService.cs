using FindingImmo.Core.Domain.DataAccess;
using FindingImmo.Core.Domain.Models;
using FindingImmo.Core.Infrastructure.Mailing;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindingImmo.Core.Services
{
    sealed internal class FindingImmoService
    {
        private readonly Mailer _mailer;
        private readonly AdsScrapingService _service;
        private readonly IAdRepository _repository;

        public FindingImmoService(AdsScrapingService service, Mailer mailer, IAdRepository repository)
        {
            this._mailer = mailer;
            this._service = service;
            this._repository = repository;
        }

        public void TryFindImmo()
        {
            IEnumerable<Ad> newAds = this._service.UpdateAll();
            string mailWithNewAds = BuildMailContent(newAds);
            this._mailer.Send("Les nouvelles maisons sont là !", mailWithNewAds);

            foreach (Ad ad in newAds)
            {
                this._repository.MarkAsSent(ad);
            }
        }

        private string BuildMailContent(IEnumerable<Ad> ads)
        {
            var builder = new StringBuilder()
                .AppendLine("<html>")
                .AppendLine("   <body>")
                .AppendLine("       <p>De nouvelles maisons sont là !</p>");

            foreach (Ad ad in ads)
                builder = builder.AppendLine(BuildAdDescription(ad));

            return builder
                .AppendLine("   </body>")
                .AppendLine("</html>")
                .ToString();
        }

        private string BuildAdDescription(Ad ad)
        {
            if (ad == null)
                throw new ArgumentNullException(nameof(ad));

            return $@"<div style='width:100%'>
                    <a href='{ad.DetailUrl}'>
                        <p>{ad.Description}</p>
                        <p><img src='{ad.PictureUrl}' /><p>
                    </a>    
                </div>";
        }
    }
}
