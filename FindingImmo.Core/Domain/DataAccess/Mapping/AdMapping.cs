using FindingImmo.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindingImmo.Core.Domain.DataAccess.Mapping
{
    public sealed class AdMapping : EntityMapping<Ad>
    {
        public override void Configure(EntityTypeBuilder<Ad> builder)
        {
            builder.ToTable("offre");
            builder.HasKey(x => x.Id);

            builder.Property(a => a.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(a => a.Creation).HasColumnName("date_creation").IsRequired();
            builder.Property(a => a.Description).HasColumnName("description").IsRequired();
            builder.Property(a => a.DetailUrl).HasColumnName("url").IsRequired();
            builder.Property(a => a.ExternalId).HasColumnName("reference_externe").IsRequired();
            builder.Property(a => a.PictureUrl).HasColumnName("url_image").IsRequired();
            builder.Property(a => a.Origin).HasColumnName("site_origine").IsRequired();
            builder.Property(a => a.State).HasColumnName("etat").HasMaxLength(5).IsRequired();
        }
    }
}
