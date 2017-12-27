using FindingImmo.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindingImmo.Core.Domain.DataAccess.Mapping
{
    public sealed class FeaturesMapping : EntityMapping<Features>
    {
        public override void Configure(EntityTypeBuilder<Features> builder)
        {
            builder.ToTable("caracteristiques");
            builder.HasKey(x => x.AdId);

            builder.Property(a => a.AdId).HasColumnName("id_offre").IsRequired();
            builder.Property(a => a.TwoFamilyHouse).HasColumnName("bifamille").IsRequired();
            builder.Property(a => a.AdId).HasColumnName("id_offre").IsRequired();
            builder.HasOne(a => a.Ad).WithOne(a => a.Features).HasForeignKey<Features>(a => a.AdId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
