using BuiltCode.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuiltCode.Infrastructure.Configuration
{
    public class ParceiroConfiguration : IEntityTypeConfiguration<Parceiro>
    {
        public void Configure(EntityTypeBuilder<Parceiro> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasColumnType("varchar(255)");
            builder.Property(x => x.ApiKey).IsRequired().HasColumnType("varchar(36)");

            builder.HasData(new Parceiro
            {
                Id = Guid.Parse("7636833c-a4fb-4a36-ae91-64ca63f2d02d"),
                Nome = "ParceiroTeste",
                ApiKey = Guid.Parse("406a17e0-277e-4926-9b62-be63fcee399f")
            });
        }

        
    }
}
