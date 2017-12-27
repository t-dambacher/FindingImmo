using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindingImmo.Core.Domain.DataAccess.Mapping
{
    public abstract class EntityMapping
    {
        public abstract void Configure(ModelBuilder modelBuilder);
    }

    public abstract class EntityMapping<TEntity> : EntityMapping, IEntityTypeConfiguration<TEntity>
         where TEntity : class
    {
        public sealed override void Configure(ModelBuilder modelBuilder)
        {
            Configure(modelBuilder.Entity<TEntity>());
        }

        public abstract void Configure(EntityTypeBuilder<TEntity> builder);
    }
}
