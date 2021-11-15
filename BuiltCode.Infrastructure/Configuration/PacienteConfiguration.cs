using BuiltCode.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuiltCode.Infrastructure.Configuration
{
    public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).HasColumnType("varchar(255)");
            builder.Property(x => x.Cpf).IsRequired().HasColumnType("varchar(11)");
            builder.HasIndex(x => x.Cpf).IsUnique();
            builder.Property(x => x.Telefone).HasColumnType("varchar(20)");
        }
    }
}
