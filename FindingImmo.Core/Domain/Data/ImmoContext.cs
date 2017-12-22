using Microsoft.EntityFrameworkCore;

namespace FindingImmo.Core.Domain.Data
{
    sealed internal class ImmoContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationFromAssembly();
        }
    }
}
