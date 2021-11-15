using BuiltCode.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuiltCode.Infrastructure.Configuration
{
    public class MedicoConfiguration : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasColumnType("varchar(255)");
            builder.Property(x => x.Crm).IsRequired().HasColumnType("varchar(50)");
            builder.Property(x => x.UfCrm).IsRequired().HasColumnType("varchar(2)");
            builder.Property(x => x.Especialidade).IsRequired().HasColumnType("varchar(255)");
            builder.HasIndex(x => new { x.Crm, x.UfCrm }).IsUnique();
            builder.HasMany(x => x.Pacientes).WithOne(x => x.Medico).HasForeignKey(x => x.MedicoId);
        }
    }
}
