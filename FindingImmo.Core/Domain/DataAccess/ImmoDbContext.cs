using FindingImmo.Core.Domain.DataAccess.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FindingImmo.Core.Domain.DataAccess
{
    sealed public class ImmoDbContext : DbContext
    {
        public static void EnsureCreated(DbContextOptions<ImmoDbContext> options)
        {
            using (ImmoDbContext context = new ImmoDbContext(options))
            {
                context.Database.EnsureCreated();

                if (context.Database.GetPendingMigrations().Any())
                    context.Database.Migrate();
            }
        }

        private ImmoDbContext()
            : base()
        {
            throw new NotSupportedException();
        }

        public ImmoDbContext(DbContextOptions<ImmoDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationFromAssembly();
        }
    }
}
