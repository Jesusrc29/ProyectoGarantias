using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectGarantia.Migrations
{
    /// <inheritdoc />
    public partial class ProyectoMuestra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Garantias",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Garantias");
        }
    }
}
