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
            builder.Property(a => a.EnergyClass).HasColumnName("classe_energie").IsRequired();
            builder.Property(a => a.ExternalId).HasColumnName("reference_externe").IsRequired();
            builder.Property(a => a.GES).HasColumnName("GES").IsRequired();
            builder.Property(a => a.IsPro).HasColumnName("est_agence").IsRequired();
            builder.Property(a => a.LastScraping).HasColumnName("date_derniere_maj").IsRequired();
            builder.Property(a => a.Origin).HasColumnName("site_origine").IsRequired();
            builder.Property(a => a.PostalCode).HasColumnName("code_postal").HasMaxLength(5).IsRequired();
            builder.Property(a => a.Price).HasColumnName("prix").IsRequired();
            builder.Property(a => a.RoomsCount).HasColumnName("nombre_pieces").IsRequired();
            builder.Property(a => a.Surface).HasColumnName("surface").IsRequired();
            builder.Property(a => a.Title).HasColumnName("titre").IsRequired();
            builder.HasMany(a => a.Pictures).WithOne(p => p.Ad).HasForeignKey(p => p.AdId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(a => a.Features).WithOne(f => f.Ad).HasForeignKey<Features>(f => f.AdId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
