using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MELC.Main.API.Migrations
{
    public partial class tabelaArquivos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArquivosDesenho",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    DesenhoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeArquivo = table.Column<string>(type: "varchar(300)", nullable: false),
                    CaminhoArquivo = table.Column<string>(type: "varchar(600)", nullable: false),
                    Extensao = table.Column<string>(type: "varchar(20)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArquivosDesenho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArquivosDesenho_Desenhos_DesenhoId",
                        column: x => x.DesenhoId,
                        principalTable: "Desenhos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArquivosDesenho_DesenhoId",
                table: "ArquivosDesenho",
                column: "DesenhoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArquivosDesenho");
        }
    }
}
