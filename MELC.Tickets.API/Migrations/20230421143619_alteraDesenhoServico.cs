using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MELC.Main.API.Migrations
{
    public partial class alteraDesenhoServico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tempo",
                table: "DesenhoServicos");

            migrationBuilder.AddColumn<int>(
                name: "Horas",
                table: "DesenhoServicos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Minutos",
                table: "DesenhoServicos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuantidadePeca",
                table: "DesenhoServicos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Horas",
                table: "DesenhoServicos");

            migrationBuilder.DropColumn(
                name: "Minutos",
                table: "DesenhoServicos");

            migrationBuilder.DropColumn(
                name: "QuantidadePeca",
                table: "DesenhoServicos");

            migrationBuilder.AddColumn<double>(
                name: "Tempo",
                table: "DesenhoServicos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
