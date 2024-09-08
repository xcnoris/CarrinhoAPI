using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarrinhoAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTableEntidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Cidade_Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UF = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Endereco_Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Endereco_Complemento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DDD_Celular = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    Sexo = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CongregacaoId = table.Column<int>(type: "int", nullable: false),
                    Data_Cadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entidades_Congregacoes_CongregacaoId",
                        column: x => x.CongregacaoId,
                        principalTable: "Congregacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entidades_CongregacaoId",
                table: "Entidades",
                column: "CongregacaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entidades");
        }
    }
}
