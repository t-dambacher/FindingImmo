using FindingImmo.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FindingImmo.Core.Domain.DataAccess
{
    public sealed class ImmoDbContextFactory : IDesignTimeDbContextFactory<ImmoDbContext>
    {
        public ImmoDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ImmoDbContext>();
            builder.UseSqlite(Configuration.ConnectionString);
            return new ImmoDbContext(builder.Options);
        }
    }
}
