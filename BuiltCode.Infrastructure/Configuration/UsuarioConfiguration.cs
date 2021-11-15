using BuiltCode.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Security.Cryptography;
using System.Text;

namespace BuiltCode.Infrastructure.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public static Usuario GerarHash(string senha)
        {
            using var hmac = new HMACSHA512();
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));
            var passwordSalt = hmac.Key;

            var user = new Usuario
            {
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            return user;
        }

        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            var admin = GerarHash("123456");
            var atendente = GerarHash("123456");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).IsRequired().HasColumnType("varchar(255)");
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Nome).IsRequired().HasColumnType("varchar(255)");
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.PasswordSalt).IsRequired();
            builder.Property(x => x.Perfil).IsRequired().HasConversion<int>();

            builder.HasData(new Usuario
            {
                Id = Guid.Parse("f511c7f1-fb35-4e31-9fc2-a873b8808e10"),
                Email = "contato@builtcode.com",
                Nome = "admin",
                PasswordHash = admin.PasswordHash,
                PasswordSalt = admin.PasswordSalt,
                Perfil = 0
            });

            builder.HasData(new Usuario
            {
                Id = Guid.Parse("f511c7f1-fb35-4e31-9fc2-a873b8808e01"),
                Email = "atendente@builtcode.com",
                Nome = "atendente",
                PasswordHash = atendente.PasswordHash,
                PasswordSalt = atendente.PasswordSalt,
                Perfil = (EPerfil)1
            });
        }
    }
}
