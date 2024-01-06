using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectGarantia.Migrations
{
    /// <inheritdoc />
    public partial class Migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesLote_Agencias_AgenciaId",
                table: "DetallesLote");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesLote_Clientes_ClienteId",
                table: "DetallesLote");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesLote_Lotes_LoteId",
                table: "DetallesLote");

            migrationBuilder.DropForeignKey(
                name: "FK_Documentaciones_DetallesLote_DetalleLoteId",
                table: "Documentaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Garantias_DetallesLote_DetalleLoteId",
                table: "Garantias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesLote",
                table: "DetallesLote");

            migrationBuilder.RenameTable(
                name: "DetallesLote",
                newName: "DetalleLote");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesLote_LoteId",
                table: "DetalleLote",
                newName: "IX_DetalleLote_LoteId");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesLote_ClienteId",
                table: "DetalleLote",
                newName: "IX_DetalleLote_ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesLote_AgenciaId",
                table: "DetalleLote",
                newName: "IX_DetalleLote_AgenciaId");

            migrationBuilder.AddColumn<string>(
                name: "NumContrato",
                table: "DetalleLoteModelo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumPagare",
                table: "DetalleLoteModelo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumPrestamo",
                table: "DetalleLoteModelo",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetalleLote",
                table: "DetalleLote",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleLote_Agencias_AgenciaId",
                table: "DetalleLote",
                column: "AgenciaId",
                principalTable: "Agencias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleLote_Clientes_ClienteId",
                table: "DetalleLote",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleLote_Lotes_LoteId",
                table: "DetalleLote",
                column: "LoteId",
                principalTable: "Lotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documentaciones_DetalleLote_DetalleLoteId",
                table: "Documentaciones",
                column: "DetalleLoteId",
                principalTable: "DetalleLote",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Garantias_DetalleLote_DetalleLoteId",
                table: "Garantias",
                column: "DetalleLoteId",
                principalTable: "DetalleLote",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleLote_Agencias_AgenciaId",
                table: "DetalleLote");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleLote_Clientes_ClienteId",
                table: "DetalleLote");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleLote_Lotes_LoteId",
                table: "DetalleLote");

            migrationBuilder.DropForeignKey(
                name: "FK_Documentaciones_DetalleLote_DetalleLoteId",
                table: "Documentaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Garantias_DetalleLote_DetalleLoteId",
                table: "Garantias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetalleLote",
                table: "DetalleLote");

            migrationBuilder.DropColumn(
                name: "NumContrato",
                table: "DetalleLoteModelo");

            migrationBuilder.DropColumn(
                name: "NumPagare",
                table: "DetalleLoteModelo");

            migrationBuilder.DropColumn(
                name: "NumPrestamo",
                table: "DetalleLoteModelo");

            migrationBuilder.RenameTable(
                name: "DetalleLote",
                newName: "DetallesLote");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleLote_LoteId",
                table: "DetallesLote",
                newName: "IX_DetallesLote_LoteId");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleLote_ClienteId",
                table: "DetallesLote",
                newName: "IX_DetallesLote_ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleLote_AgenciaId",
                table: "DetallesLote",
                newName: "IX_DetallesLote_AgenciaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesLote",
                table: "DetallesLote",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesLote_Agencias_AgenciaId",
                table: "DetallesLote",
                column: "AgenciaId",
                principalTable: "Agencias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesLote_Clientes_ClienteId",
                table: "DetallesLote",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesLote_Lotes_LoteId",
                table: "DetallesLote",
                column: "LoteId",
                principalTable: "Lotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documentaciones_DetallesLote_DetalleLoteId",
                table: "Documentaciones",
                column: "DetalleLoteId",
                principalTable: "DetallesLote",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Garantias_DetallesLote_DetalleLoteId",
                table: "Garantias",
                column: "DetalleLoteId",
                principalTable: "DetallesLote",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
