using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MELC.Main.API.Migrations
{
    public partial class addMateriaisESolidos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Materiais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Densidade = table.Column<double>(type: "float", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materiais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Solidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Largura = table.Column<double>(type: "float", nullable: true),
                    Altura = table.Column<double>(type: "float", nullable: true),
                    Comprimento = table.Column<double>(type: "float", nullable: false),
                    Expessura = table.Column<double>(type: "float", nullable: true),
                    ExpessuraSuperior = table.Column<double>(type: "float", nullable: true),
                    Diametro = table.Column<double>(type: "float", nullable: true),
                    Area = table.Column<double>(type: "float", nullable: false),
                    Volume = table.Column<double>(type: "float", nullable: false),
                    TipoSolido = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MateriaisDesenhos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    DesenhoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CriadoPorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SolidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantidade = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriaisDesenhos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MateriaisDesenhos_Desenhos_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Desenhos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MateriaisDesenhos_Materiais_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materiais",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MateriaisDesenhos_Solidos_SolidoId",
                        column: x => x.SolidoId,
                        principalTable: "Solidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MateriaisDesenhos_Users_CriadoPorId",
                        column: x => x.CriadoPorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MateriaisDesenhos_CriadoPorId",
                table: "MateriaisDesenhos",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaisDesenhos_MaterialId",
                table: "MateriaisDesenhos",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaisDesenhos_SolidoId",
                table: "MateriaisDesenhos",
                column: "SolidoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MateriaisDesenhos");

            migrationBuilder.DropTable(
                name: "Materiais");

            migrationBuilder.DropTable(
                name: "Solidos");
        }
    }
}
