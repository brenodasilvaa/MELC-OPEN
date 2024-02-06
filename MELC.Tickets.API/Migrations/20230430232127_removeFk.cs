using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MELC.Main.API.Migrations
{
    public partial class removeFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MateriaisDesenhos_Desenhos_MaterialId",
                table: "MateriaisDesenhos");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaisDesenhos_DesenhoId",
                table: "MateriaisDesenhos",
                column: "DesenhoId");

            migrationBuilder.AddForeignKey(
                name: "FK_MateriaisDesenhos_Desenhos_DesenhoId",
                table: "MateriaisDesenhos",
                column: "DesenhoId",
                principalTable: "Desenhos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MateriaisDesenhos_Desenhos_DesenhoId",
                table: "MateriaisDesenhos");

            migrationBuilder.DropIndex(
                name: "IX_MateriaisDesenhos_DesenhoId",
                table: "MateriaisDesenhos");

            migrationBuilder.AddForeignKey(
                name: "FK_MateriaisDesenhos_Desenhos_MaterialId",
                table: "MateriaisDesenhos",
                column: "MaterialId",
                principalTable: "Desenhos",
                principalColumn: "Id");
        }
    }
}
