using FindingImmo.Core.Infrastructure;
using FindingImmo.Core.Infrastructure.Logging;
using FindingImmo.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using static System.Console;

namespace FindingImmo.Console
{
    public static class Program
    {
        private static IServiceProvider ServiceProvider { get; } = Bootstrap();

        public static void Main(string[] args)
        {
            ILogger logger = ServiceProvider.GetService<ILogger>();
            FindingImmoService service = ServiceProvider.GetService<FindingImmoService>();

            try
            {
                service.TryFindImmo();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                ReadKey();
            }
        }

        private static IServiceProvider Bootstrap()
        {
            return Startup.Bootstrap(new ServiceCollection());
        }
    }
}
