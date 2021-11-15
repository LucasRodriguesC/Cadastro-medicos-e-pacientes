using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BuiltCode.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "varchar(255)", nullable: false),
                    Crm = table.Column<string>(type: "varchar(50)", nullable: false),
                    UfCrm = table.Column<string>(type: "varchar(2)", nullable: false),
                    Especialidade = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parceiros",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "varchar(255)", nullable: false),
                    ApiKey = table.Column<string>(type: "varchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parceiros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(255)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    Perfil = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "varchar(255)", nullable: true),
                    Cpf = table.Column<string>(type: "varchar(11)", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(20)", nullable: true),
                    MedicoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pacientes_Medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Parceiros",
                columns: new[] { "Id", "ApiKey", "Nome" },
                values: new object[] { new Guid("7636833c-a4fb-4a36-ae91-64ca63f2d02d"), "406a17e0-277e-4926-9b62-be63fcee399f", "ParceiroTeste" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Email", "Nome", "PasswordHash", "PasswordSalt", "Perfil" },
                values: new object[,]
                {
                    { new Guid("f511c7f1-fb35-4e31-9fc2-a873b8808e10"), "contato@builtcode.com", "admin", new byte[] { 157, 165, 83, 0, 64, 102, 112, 63, 131, 95, 52, 110, 51, 251, 202, 140, 14, 206, 149, 10, 136, 165, 113, 25, 7, 77, 22, 178, 198, 46, 130, 106, 151, 147, 203, 223, 83, 245, 171, 158, 32, 167, 212, 58, 146, 204, 120, 139, 208, 10, 157, 17, 65, 231, 82, 65, 160, 203, 191, 28, 181, 78, 39, 130 }, new byte[] { 243, 4, 230, 56, 171, 197, 91, 24, 79, 140, 252, 65, 252, 201, 168, 179, 246, 200, 92, 171, 4, 15, 168, 96, 193, 180, 117, 218, 234, 247, 80, 242, 254, 91, 123, 59, 124, 162, 10, 93, 3, 180, 246, 253, 61, 138, 214, 31, 5, 153, 148, 213, 98, 39, 143, 11, 121, 131, 212, 159, 252, 17, 240, 165, 58, 93, 16, 108, 220, 209, 202, 213, 47, 35, 41, 139, 122, 44, 233, 239, 92, 93, 150, 153, 248, 133, 230, 69, 37, 145, 90, 93, 254, 169, 50, 12, 244, 192, 61, 122, 13, 24, 231, 164, 2, 173, 249, 58, 80, 40, 65, 193, 249, 147, 89, 254, 87, 125, 30, 66, 243, 47, 211, 241, 220, 159, 131, 166 }, 0 },
                    { new Guid("f511c7f1-fb35-4e31-9fc2-a873b8808e01"), "atendente@builtcode.com", "atendente", new byte[] { 226, 201, 70, 74, 253, 37, 151, 105, 34, 48, 42, 122, 111, 7, 62, 220, 232, 157, 110, 100, 12, 31, 71, 177, 36, 172, 241, 57, 190, 180, 93, 15, 208, 223, 210, 154, 211, 22, 69, 254, 88, 88, 95, 205, 247, 30, 68, 11, 72, 212, 76, 235, 63, 239, 48, 8, 117, 163, 82, 202, 223, 136, 40, 54 }, new byte[] { 65, 141, 146, 140, 45, 169, 213, 171, 232, 52, 145, 208, 104, 203, 147, 56, 215, 109, 109, 34, 115, 210, 243, 126, 131, 70, 219, 110, 106, 55, 220, 171, 43, 222, 18, 70, 233, 42, 91, 163, 157, 39, 172, 99, 234, 203, 14, 144, 22, 250, 28, 183, 72, 18, 182, 25, 78, 14, 69, 190, 102, 252, 122, 180, 91, 129, 69, 19, 2, 228, 35, 241, 209, 123, 8, 135, 43, 125, 63, 32, 186, 157, 29, 151, 68, 144, 71, 185, 132, 144, 133, 144, 191, 136, 158, 24, 200, 93, 61, 104, 220, 98, 98, 41, 78, 206, 14, 100, 142, 95, 63, 144, 188, 38, 22, 54, 157, 134, 73, 75, 99, 78, 177, 180, 242, 119, 47, 123 }, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_Crm_UfCrm",
                table: "Medicos",
                columns: new[] { "Crm", "UfCrm" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_Cpf",
                table: "Pacientes",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_MedicoId",
                table: "Pacientes",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Parceiros");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Medicos");
        }
    }
}
