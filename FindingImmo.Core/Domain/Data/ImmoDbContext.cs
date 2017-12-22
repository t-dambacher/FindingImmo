using Microsoft.EntityFrameworkCore;
using System;

namespace FindingImmo.Core.Domain.Data
{
    sealed internal class ImmoDbContext : DbContext
    {
        public static void EnsureCreated(DbContextOptions<ImmoDbContext> options)
        {
            using (ImmoDbContext context = new ImmoDbContext(options))
            {
                context.Database.EnsureCreated();
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
