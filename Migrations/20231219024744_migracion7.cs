using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectGarantia.Migrations
{
    /// <inheritdoc />
    public partial class migracion7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescripAval",
                table: "Garantias",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "MontoGarantia",
                table: "Garantias",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "NombreAval",
                table: "Garantias",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescripAval",
                table: "Garantias");

            migrationBuilder.DropColumn(
                name: "MontoGarantia",
                table: "Garantias");

            migrationBuilder.DropColumn(
                name: "NombreAval",
                table: "Garantias");
        }
    }
}
