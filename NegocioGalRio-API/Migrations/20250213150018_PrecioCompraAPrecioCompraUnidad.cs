using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NegocioGalRio_API.Migrations
{
    /// <inheritdoc />
    public partial class PrecioCompraAPrecioCompraUnidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrecioCompra",
                table: "Productos",
                newName: "PrecioCompraUnidad");

            migrationBuilder.RenameColumn(
                name: "PrecioCompra",
                table: "DetalleCompras",
                newName: "Subtotal");

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioCompraUnidad",
                table: "DetalleCompras",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioCompraUnidad",
                table: "DetalleCompras");

            migrationBuilder.RenameColumn(
                name: "PrecioCompraUnidad",
                table: "Productos",
                newName: "PrecioCompra");

            migrationBuilder.RenameColumn(
                name: "Subtotal",
                table: "DetalleCompras",
                newName: "PrecioCompra");
        }
    }
}
