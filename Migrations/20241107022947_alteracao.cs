using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcVendas.Migrations
{
    /// <inheritdoc />
    public partial class alteracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plantios_Recursos_ProdutoFinalId",
                table: "Plantios");

            migrationBuilder.DropIndex(
                name: "IX_Plantios_ProdutoFinalId",
                table: "Plantios");

            migrationBuilder.DropColumn(
                name: "ProdutoFinalId",
                table: "Plantios");

            migrationBuilder.DropColumn(
                name: "NumeroSerie",
                table: "ItemsPlantio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProdutoFinalId",
                table: "Plantios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NumeroSerie",
                table: "ItemsPlantio",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plantios_ProdutoFinalId",
                table: "Plantios",
                column: "ProdutoFinalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plantios_Recursos_ProdutoFinalId",
                table: "Plantios",
                column: "ProdutoFinalId",
                principalTable: "Recursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
