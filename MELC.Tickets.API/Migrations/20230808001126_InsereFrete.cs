using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MELC.Main.API.Migrations
{
    public partial class InsereFrete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FretesDesenhos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    DesenhoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CriadoPorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FretesDesenhos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FretesDesenhos_Desenhos_DesenhoId",
                        column: x => x.DesenhoId,
                        principalTable: "Desenhos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FretesDesenhos_Users_CriadoPorId",
                        column: x => x.CriadoPorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FretesDesenhos_CriadoPorId",
                table: "FretesDesenhos",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_FretesDesenhos_DesenhoId",
                table: "FretesDesenhos",
                column: "DesenhoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FretesDesenhos");
        }
    }
}
