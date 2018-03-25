using FindingImmo.Core.Domain.DataAccess;
using FindingImmo.Core.Infrastructure.Logging;
using FindingImmo.Core.Infrastructure.Mailing;
using FindingImmo.Core.Scraping.Sites;
using FindingImmo.Core.Scraping.Sites.Absis;
using FindingImmo.Core.Scraping.Sites.AdsImmo;
using FindingImmo.Core.Scraping.Sites.AIS;
using FindingImmo.Core.Scraping.Sites.AIW;
using FindingImmo.Core.Scraping.Sites.AlphaPatrimoine;
using FindingImmo.Core.Scraping.Sites.Arp;
using FindingImmo.Core.Scraping.Sites.AugusteImmo;
using FindingImmo.Core.Scraping.Sites.AutrementImmobilier;
using FindingImmo.Core.Scraping.Sites.B2L;
using FindingImmo.Core.Scraping.Sites.Baumann;
using FindingImmo.Core.Scraping.Sites.Bitz;
using FindingImmo.Core.Scraping.Sites.Boistelle;
using FindingImmo.Core.Scraping.Sites.CapEstImmo;
using FindingImmo.Core.Scraping.Sites.Capifrance;
using FindingImmo.Core.Scraping.Sites.Century21;
using FindingImmo.Core.Scraping.Sites.Domial;
using FindingImmo.Core.Scraping.Sites.Eurotransactions;
using FindingImmo.Core.Scraping.Sites.Fnaim;
using FindingImmo.Core.Scraping.Sites.Foncia;
using FindingImmo.Core.Scraping.Sites.Groupimmo;
using FindingImmo.Core.Scraping.Sites.GuyHoquet;
using FindingImmo.Core.Scraping.Sites.Heckmann;
using FindingImmo.Core.Scraping.Sites.HertrichEtKern;
using FindingImmo.Core.Scraping.Sites.Hrd;
using FindingImmo.Core.Scraping.Sites.IEVC;
using FindingImmo.Core.Scraping.Sites.IMBS;
using FindingImmo.Core.Scraping.Sites.ImmobiliereDeHanau;
using FindingImmo.Core.Scraping.Sites.ImmobiliereDesRohan;
using FindingImmo.Core.Scraping.Sites.Immobiz;
using FindingImmo.Core.Scraping.Sites.ImmoGeyer;
using FindingImmo.Core.Scraping.Sites.Immosurmesure;
using FindingImmo.Core.Scraping.Sites.ImmoTeam;
using FindingImmo.Core.Scraping.Sites.LaChenaieImmobilier;
using FindingImmo.Core.Scraping.Sites.Laemmel;
using FindingImmo.Core.Scraping.Sites.LaLicorne;
using FindingImmo.Core.Scraping.Sites.Logia;
using FindingImmo.Core.Scraping.Sites.MapsImmo;
using FindingImmo.Core.Scraping.Sites.Mehl;
using FindingImmo.Core.Scraping.Sites.Mercor;
using FindingImmo.Core.Scraping.Sites.OrpiBrumath;
using FindingImmo.Core.Scraping.Sites.PrestigeImmo;
using FindingImmo.Core.Scraping.Sites.QuatrePourcentsImmobilier;
using FindingImmo.Core.Scraping.Sites.RueDeLImmobilier;
using FindingImmo.Core.Scraping.Sites.Salomon;
using FindingImmo.Core.Scraping.Sites.Shorp;
using FindingImmo.Core.Scraping.Sites.Siihe;
using FindingImmo.Core.Scraping.Sites.SolutionsImmo;
using FindingImmo.Core.Scraping.Sites.Solvimmo;
using FindingImmo.Core.Scraping.Sites.StephanePlaza;
using FindingImmo.Core.Scraping.Sites.Trois3Gimmobilier;
using FindingImmo.Core.Scraping.Sites.TroisPourcentsPointCom;
using FindingImmo.Core.Scraping.Sites.Upperside;
using FindingImmo.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FindingImmo.Core.Infrastructure.DependencyInjection
{
    public static class DependenciesConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            ConfigureInfratructure(services);
            ConfigureServices(services);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<AdsScrapingService>();
            services.AddTransient<FindingImmoService>();
            ConfigureScrappers(services);
        }

        private static void ConfigureDataAccess(IServiceCollection services)
        {
            services.AddTransient<IAdRepository, AdRepository>();
            services.AddDbContext<ImmoDbContext>(options => options.UseSqlite(Configuration.ConnectionString));
        }

        private static void ConfigureInfratructure(IServiceCollection services)
        {
            services.AddSingleton<ILogger>(Logger.Instance);
            services.AddSingleton<Mailer>();
            ConfigureDataAccess(services);
        }

        private static void ConfigureScrappers(IServiceCollection services)
        {
            //services.AddTransient<AdReferencesScraper, TroisGimmobilierScrapper>();
            //services.AddTransient<AdReferencesScraper, TroisPourcentsPointComScrapper>();
            //services.AddTransient<AdReferencesScraper, QuatrePourcentsImmobilierScrapper>();
            //services.AddTransient<AdReferencesScraper, AbsisScrapper>();
            //services.AddTransient<AdReferencesScraper, AdsImmoScrapper>();
            //services.AddTransient<AdReferencesScraper, AISScrapper>();
            //services.AddTransient<AdReferencesScraper, AIWScrapper>();
            //services.AddTransient<AdReferencesScraper, AlphaPatrimoineScrapper>();
            //services.AddTransient<AdReferencesScraper, ArpScrapper>();
            //services.AddTransient<AdReferencesScraper, AugusteImmoScrapper>();
            //services.AddTransient<AdReferencesScraper, AutrementImmobilierScrapper>();
            //services.AddTransient<AdReferencesScraper, B2LScrapper>();
            //services.AddTransient<AdReferencesScraper, BaumannScrapper>();
            //services.AddTransient<AdReferencesScraper, BitzScrapper>();
            //services.AddTransient<AdReferencesScraper, BoistelleScrapper>();
            //services.AddTransient<AdReferencesScraper, CapEstImmoScrapper>();
            //services.AddTransient<AdReferencesScraper, CapifranceScrapper>();
            //services.AddTransient<AdReferencesScraper, Century21Scrapper>();
            //services.AddTransient<AdReferencesScraper, DomialScrapper>();
            //services.AddTransient<AdReferencesScraper, EurotransactionsScrapper>();
            //services.AddTransient<AdReferencesScraper, FnaimScrapper>();
            //services.AddTransient<AdReferencesScraper, FonciaScrapper>();
            //services.AddTransient<AdReferencesScraper, GroupimmoScrapper>();
            //services.AddTransient<AdReferencesScraper, GuyHoquetScrapper>();
            //services.AddTransient<AdReferencesScraper, HeckmannScrapper>();
            //services.AddTransient<AdReferencesScraper, HertrichEtKernScrapper>();
            //services.AddTransient<AdReferencesScraper, HrdScrapper>();
            //services.AddTransient<AdReferencesScraper, IEVCScrapper>();
            //services.AddTransient<AdReferencesScraper, IMBSScrapper>();
            //services.AddTransient<AdReferencesScraper, ImmobiliereDeHanauScrapper>();
            //services.AddTransient<AdReferencesScraper, ImmobiliereDesRohanScrapper>();
            //services.AddTransient<AdReferencesScraper, ImmobizScrapper>();
            //services.AddTransient<AdReferencesScraper, ImmoGeyerScrapper>();
            //services.AddTransient<AdReferencesScraper, ImmosurmesureScrapper>();
            //services.AddTransient<AdReferencesScraper, ImmoTeamScrapper>();
            //services.AddTransient<AdReferencesScraper, LaChenaieImmobilierScrapper>();
            //services.AddTransient<AdReferencesScraper, LaemmelScrapper>();
            //services.AddTransient<AdReferencesScraper, LaLicorneScrapper>();
            //services.AddTransient<AdReferencesScraper, LogiaScrapper>();
            //services.AddTransient<AdReferencesScraper, MapsImmoScrapper>();
            //services.AddTransient<AdReferencesScraper, MehlScrapper>();
            //services.AddTransient<AdReferencesScraper, MercorScrapper>();
            services.AddTransient<AdReferencesScraper, OrpiBrumathScrapper>();
            //services.AddTransient<AdReferencesScraper, PrestigeImmoScrapper>();
            //services.AddTransient<AdReferencesScraper, RueDeLImmobilierScrapper>();
            //services.AddTransient<AdReferencesScraper, SalomonScrapper>();
            //services.AddTransient<AdReferencesScraper, ShorpScrapper>();
            //services.AddTransient<AdReferencesScraper, SiiheScrapper>();
            //services.AddTransient<AdReferencesScraper, SolutionsImmoScrapper>();
            //services.AddTransient<AdReferencesScraper, SolvimmoScrapper>();
            //services.AddTransient<AdReferencesScraper, StephanePlazaScrapper>();
            //services.AddTransient<AdReferencesScraper, UppersideScrapper>();
        }
    }
}
