using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NegocioGalRio_API.Migrations
{
    /// <inheritdoc />
    public partial class FechaRegistroStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioRoles",
                table: "UsuarioRoles");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioRoles_UsuarioId",
                table: "UsuarioRoles");

            migrationBuilder.DropColumn(
                name: "UsuarioDNI",
                table: "UsuarioRoles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioRoles",
                table: "UsuarioRoles",
                columns: new[] { "UsuarioId", "RolId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioRoles",
                table: "UsuarioRoles");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioDNI",
                table: "UsuarioRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioRoles",
                table: "UsuarioRoles",
                columns: new[] { "UsuarioDNI", "RolId" });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRoles_UsuarioId",
                table: "UsuarioRoles",
                column: "UsuarioId");
        }
    }
}
