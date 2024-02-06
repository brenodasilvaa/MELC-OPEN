using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MELC.Main.API.Migrations
{
    public partial class InsereJurosImpostosDesenha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Impostos",
                table: "Desenhos",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Lucro",
                table: "Desenhos",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Impostos",
                table: "Desenhos");

            migrationBuilder.DropColumn(
                name: "Lucro",
                table: "Desenhos");
        }
    }
}
