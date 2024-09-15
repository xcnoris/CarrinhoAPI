using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarrinhoAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddColunAtualizaoCateriaAgendamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Categoria_Agendamento",
                newName: "Data_Criacao");

            migrationBuilder.AddColumn<DateTime>(
                name: "Data_Atualizacao",
                table: "Categoria_Agendamento",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data_Atualizacao",
                table: "Categoria_Agendamento");

            migrationBuilder.RenameColumn(
                name: "Data_Criacao",
                table: "Categoria_Agendamento",
                newName: "DataCriacao");
        }
    }
}
