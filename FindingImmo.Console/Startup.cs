using FindingImmo.Core.Domain.DataAccess;
using FindingImmo.Core.Infrastructure;
using FindingImmo.Core.Infrastructure.DependencyInjection;
using FindingImmo.Core.Services;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FindingImmo.Console
{
    public class Startup : IStartup
    {
        private readonly IHostingEnvironment environment;

        public Startup(IHostingEnvironment environment)
        {
            this.environment = environment;

#if DEBUG
            this.environment.EnvironmentName = "Development";
#endif
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvcWithDefaultRoute()
                .UseHangfireServer();

            if (this.environment.IsDevelopment())
                app.UseDeveloperExceptionPage();

            RecurringJob.AddOrUpdate<FindingImmoService>(srv => srv.TryFindImmo(), Cron.Daily);

            if (this.environment.IsDevelopment())
                BackgroundJob.Enqueue<FindingImmoService>(srv => srv.TryFindImmo());
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddAntiforgery()
                .AddHttpContextAccessor()
                .AddHangfire(opt => opt.UseStorage(GlobalConfiguration.Configuration.UseMemoryStorage()))
                .AddMvc();

            Configuration.Bootstrap();
            DependenciesConfiguration.Configure(services);
            IServiceProvider provider = services.BuildServiceProvider();
            ImmoDbContext.EnsureCreated(provider.GetService<DbContextOptions<ImmoDbContext>>());

            return provider;
        }
    }
}
