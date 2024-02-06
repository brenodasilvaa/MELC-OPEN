using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MELC.Main.API.Migrations
{
    public partial class insereDesenhoServico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesenhoItems");

            migrationBuilder.CreateTable(
                name: "DesenhoServicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    DesenhoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CriadoPorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tempo = table.Column<double>(type: "float", nullable: false),
                    Servico = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesenhoServicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesenhoServicos_Desenhos_DesenhoId",
                        column: x => x.DesenhoId,
                        principalTable: "Desenhos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesenhoServicos_Users_CriadoPorId",
                        column: x => x.CriadoPorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DesenhoServicos_CriadoPorId",
                table: "DesenhoServicos",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_DesenhoServicos_DesenhoId",
                table: "DesenhoServicos",
                column: "DesenhoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesenhoServicos");

            migrationBuilder.CreateTable(
                name: "DesenhoItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    CriadoPorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DesenhoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nome = table.Column<string>(type: "varchar(300)", nullable: false),
                    Quantidade = table.Column<double>(type: "float", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesenhoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesenhoItems_Desenhos_DesenhoId",
                        column: x => x.DesenhoId,
                        principalTable: "Desenhos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesenhoItems_Users_CriadoPorId",
                        column: x => x.CriadoPorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DesenhoItems_CriadoPorId",
                table: "DesenhoItems",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_DesenhoItems_DesenhoId",
                table: "DesenhoItems",
                column: "DesenhoId");
        }
    }
}
