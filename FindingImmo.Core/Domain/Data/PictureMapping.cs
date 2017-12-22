using FindingImmo.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindingImmo.Core.Domain.Data
{
    public sealed class PictureMapping : EntityMapping<Picture>
    {
        public override void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.ToTable("image");
            builder.HasKey(x => x.Id);

            builder.Property(a => a.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(a => a.Width).HasColumnName("largeur").IsRequired();
            builder.Property(a => a.Height).HasColumnName("hauteur").IsRequired();
            builder.Property(a => a.Url).HasColumnName("url").IsRequired();
            builder.Property(a => a.AdId).HasColumnName("id_offre").IsRequired();
            builder.HasOne(a => a.Ad).WithMany(a => a.Pictures).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
