using FindingImmo.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindingImmo.Core.Domain.Data
{
    public sealed class AdMapping : EntityMapping<Ad>
    {
        public override void Configure(EntityTypeBuilder<Ad> builder)
        {
            builder.ToTable("offre");
            builder.HasKey(x => x.Id);

            builder.Property(a => a.Id).HasColumnName("id").ValueGeneratedOnAdd();
            
        }
    }
}
