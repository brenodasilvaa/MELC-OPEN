using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MELC.Main.API.Migrations
{
    public partial class modificaRelacaoUserDesenho : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesenhoUser");

            migrationBuilder.CreateIndex(
                name: "IX_Desenhos_CriadoPorId",
                table: "Desenhos",
                column: "CriadoPorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Desenhos_Users_CriadoPorId",
                table: "Desenhos",
                column: "CriadoPorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Desenhos_Users_CriadoPorId",
                table: "Desenhos");

            migrationBuilder.DropIndex(
                name: "IX_Desenhos_CriadoPorId",
                table: "Desenhos");

            migrationBuilder.CreateTable(
                name: "DesenhoUser",
                columns: table => new
                {
                    DesenhosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuariosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesenhoUser", x => new { x.DesenhosId, x.UsuariosId });
                    table.ForeignKey(
                        name: "FK_DesenhoUser_Desenhos_DesenhosId",
                        column: x => x.DesenhosId,
                        principalTable: "Desenhos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesenhoUser_Users_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DesenhoUser_UsuariosId",
                table: "DesenhoUser",
                column: "UsuariosId");
        }
    }
}
