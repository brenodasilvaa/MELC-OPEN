using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MELC.Main.API.Migrations
{
    public partial class addPesoEValor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Peso",
                table: "MateriaisDesenhos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "MateriaisDesenhos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Peso",
                table: "MateriaisDesenhos");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "MateriaisDesenhos");
        }
    }
}
