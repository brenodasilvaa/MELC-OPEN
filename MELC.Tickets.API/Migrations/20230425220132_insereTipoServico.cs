using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MELC.Main.API.Migrations
{
    public partial class insereTipoServico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Servico",
                table: "DesenhoServicos");

            migrationBuilder.AddColumn<Guid>(
                name: "TipoServicoId",
                table: "DesenhoServicos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "TiposServicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Servico = table.Column<string>(type: "varchar(100)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposServicos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DesenhoServicos_TipoServicoId",
                table: "DesenhoServicos",
                column: "TipoServicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DesenhoServicos_TiposServicos_TipoServicoId",
                table: "DesenhoServicos",
                column: "TipoServicoId",
                principalTable: "TiposServicos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DesenhoServicos_TiposServicos_TipoServicoId",
                table: "DesenhoServicos");

            migrationBuilder.DropTable(
                name: "TiposServicos");

            migrationBuilder.DropIndex(
                name: "IX_DesenhoServicos_TipoServicoId",
                table: "DesenhoServicos");

            migrationBuilder.DropColumn(
                name: "TipoServicoId",
                table: "DesenhoServicos");

            migrationBuilder.AddColumn<int>(
                name: "Servico",
                table: "DesenhoServicos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
