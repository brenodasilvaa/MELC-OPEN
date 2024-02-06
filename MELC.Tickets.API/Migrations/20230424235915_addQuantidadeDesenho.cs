using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MELC.Main.API.Migrations
{
    public partial class addQuantidadeDesenho : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantidadePeca",
                table: "DesenhoServicos");

            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "Desenhos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "Desenhos");

            migrationBuilder.AddColumn<int>(
                name: "QuantidadePeca",
                table: "DesenhoServicos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
