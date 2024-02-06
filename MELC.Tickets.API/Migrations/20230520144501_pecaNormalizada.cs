using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MELC.Main.API.Migrations
{
    public partial class pecaNormalizada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PecasNormalizadas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    DesenhoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CriadoPorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", nullable: false),
                    Quantidade = table.Column<double>(type: "float", nullable: false),
                    Preco = table.Column<double>(type: "float", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PecasNormalizadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PecasNormalizadas_Desenhos_DesenhoId",
                        column: x => x.DesenhoId,
                        principalTable: "Desenhos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PecasNormalizadas_Users_CriadoPorId",
                        column: x => x.CriadoPorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PecasNormalizadas_CriadoPorId",
                table: "PecasNormalizadas",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_PecasNormalizadas_DesenhoId",
                table: "PecasNormalizadas",
                column: "DesenhoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PecasNormalizadas");
        }
    }
}
