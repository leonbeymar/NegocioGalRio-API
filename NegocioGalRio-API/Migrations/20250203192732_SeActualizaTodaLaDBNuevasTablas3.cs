using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NegocioGalRio_API.Migrations
{
    /// <inheritdoc />
    public partial class SeActualizaTodaLaDBNuevasTablas3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Proveedors_ProveedorId",
                table: "Compras");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleCompras_Compras_CompraId",
                table: "DetalleCompras");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleCompras_Productos_ProductoId",
                table: "DetalleCompras");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleVentas_Productos_ProductoId",
                table: "DetalleVentas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleVentas_Ventas_VentaId",
                table: "DetalleVentas");

            migrationBuilder.DropIndex(
                name: "IX_DetalleVentas_ProductoId",
                table: "DetalleVentas");

            migrationBuilder.DropIndex(
                name: "IX_DetalleVentas_VentaId",
                table: "DetalleVentas");

            migrationBuilder.DropIndex(
                name: "IX_DetalleCompras_CompraId",
                table: "DetalleCompras");

            migrationBuilder.DropIndex(
                name: "IX_DetalleCompras_ProductoId",
                table: "DetalleCompras");

            migrationBuilder.DropIndex(
                name: "IX_Compras_ProveedorId",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "DetalleVentas");

            migrationBuilder.DropColumn(
                name: "VentaId",
                table: "DetalleVentas");

            migrationBuilder.DropColumn(
                name: "CompraId",
                table: "DetalleCompras");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "DetalleCompras");

            migrationBuilder.DropColumn(
                name: "ProveedorId",
                table: "Compras");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_IdProducto",
                table: "DetalleVentas",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_IdVenta",
                table: "DetalleVentas",
                column: "IdVenta");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompras_IdCompra",
                table: "DetalleCompras",
                column: "IdCompra");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompras_IdProducto",
                table: "DetalleCompras",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_IdProveedor",
                table: "Compras",
                column: "IdProveedor");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Proveedors_IdProveedor",
                table: "Compras",
                column: "IdProveedor",
                principalTable: "Proveedors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleCompras_Compras_IdCompra",
                table: "DetalleCompras",
                column: "IdCompra",
                principalTable: "Compras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleCompras_Productos_IdProducto",
                table: "DetalleCompras",
                column: "IdProducto",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleVentas_Productos_IdProducto",
                table: "DetalleVentas",
                column: "IdProducto",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleVentas_Ventas_IdVenta",
                table: "DetalleVentas",
                column: "IdVenta",
                principalTable: "Ventas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Proveedors_IdProveedor",
                table: "Compras");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleCompras_Compras_IdCompra",
                table: "DetalleCompras");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleCompras_Productos_IdProducto",
                table: "DetalleCompras");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleVentas_Productos_IdProducto",
                table: "DetalleVentas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleVentas_Ventas_IdVenta",
                table: "DetalleVentas");

            migrationBuilder.DropIndex(
                name: "IX_DetalleVentas_IdProducto",
                table: "DetalleVentas");

            migrationBuilder.DropIndex(
                name: "IX_DetalleVentas_IdVenta",
                table: "DetalleVentas");

            migrationBuilder.DropIndex(
                name: "IX_DetalleCompras_IdCompra",
                table: "DetalleCompras");

            migrationBuilder.DropIndex(
                name: "IX_DetalleCompras_IdProducto",
                table: "DetalleCompras");

            migrationBuilder.DropIndex(
                name: "IX_Compras_IdProveedor",
                table: "Compras");

            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "DetalleVentas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VentaId",
                table: "DetalleVentas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompraId",
                table: "DetalleCompras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "DetalleCompras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProveedorId",
                table: "Compras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_ProductoId",
                table: "DetalleVentas",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_VentaId",
                table: "DetalleVentas",
                column: "VentaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompras_CompraId",
                table: "DetalleCompras",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompras_ProductoId",
                table: "DetalleCompras",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_ProveedorId",
                table: "Compras",
                column: "ProveedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Proveedors_ProveedorId",
                table: "Compras",
                column: "ProveedorId",
                principalTable: "Proveedors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleCompras_Compras_CompraId",
                table: "DetalleCompras",
                column: "CompraId",
                principalTable: "Compras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleCompras_Productos_ProductoId",
                table: "DetalleCompras",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleVentas_Productos_ProductoId",
                table: "DetalleVentas",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleVentas_Ventas_VentaId",
                table: "DetalleVentas",
                column: "VentaId",
                principalTable: "Ventas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
