using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProyectGarantia.Migrations
{
    /// <inheritdoc />
    public partial class scriptnuevo1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documento_DetalleLoteModelo_Id",
                table: "Documento");

            migrationBuilder.DropForeignKey(
                name: "FK_Garantias_Almacenes_AlmacenId",
                table: "Garantias");

            migrationBuilder.DropForeignKey(
                name: "FK_Garantias_DetalleLote_DetalleLoteId",
                table: "Garantias");

            migrationBuilder.DropIndex(
                name: "IX_Garantias_AlmacenId",
                table: "Garantias");

            migrationBuilder.DropIndex(
                name: "IX_Garantias_DetalleLoteId",
                table: "Garantias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documento",
                table: "Documento");

            migrationBuilder.DropIndex(
                name: "IX_Documento_Id",
                table: "Documento");

            migrationBuilder.DropColumn(
                name: "AlmacenId",
                table: "Garantias");

            migrationBuilder.DropColumn(
                name: "DetalleLoteId",
                table: "Garantias");

            migrationBuilder.DropColumn(
                name: "DocumentoId",
                table: "Documento");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Garantias",
                newName: "PrestamoId");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "Garantias",
                newName: "DetalleLoteModeloId");

            migrationBuilder.AddColumn<string>(
                name: "CorrGarantia",
                table: "Garantias",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Documento",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id1",
                table: "Documento",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "DetalleLoteModelo",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documento",
                table: "Documento",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Garantias_DetalleLoteModeloId",
                table: "Garantias",
                column: "DetalleLoteModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Documento_Id1",
                table: "Documento",
                column: "Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_Documento_DetalleLoteModelo_Id1",
                table: "Documento",
                column: "Id1",
                principalTable: "DetalleLoteModelo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Garantias_DetalleLoteModelo_DetalleLoteModeloId",
                table: "Garantias",
                column: "DetalleLoteModeloId",
                principalTable: "DetalleLoteModelo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documento_DetalleLoteModelo_Id1",
                table: "Documento");

            migrationBuilder.DropForeignKey(
                name: "FK_Garantias_DetalleLoteModelo_DetalleLoteModeloId",
                table: "Garantias");

            migrationBuilder.DropIndex(
                name: "IX_Garantias_DetalleLoteModeloId",
                table: "Garantias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documento",
                table: "Documento");

            migrationBuilder.DropIndex(
                name: "IX_Documento_Id1",
                table: "Documento");

            migrationBuilder.DropColumn(
                name: "CorrGarantia",
                table: "Garantias");

            migrationBuilder.DropColumn(
                name: "Id1",
                table: "Documento");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "DetalleLoteModelo");

            migrationBuilder.RenameColumn(
                name: "PrestamoId",
                table: "Garantias",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "DetalleLoteModeloId",
                table: "Garantias",
                newName: "Estado");

            migrationBuilder.AddColumn<int>(
                name: "AlmacenId",
                table: "Garantias",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DetalleLoteId",
                table: "Garantias",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Documento",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "DocumentoId",
                table: "Documento",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documento",
                table: "Documento",
                column: "DocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Garantias_AlmacenId",
                table: "Garantias",
                column: "AlmacenId");

            migrationBuilder.CreateIndex(
                name: "IX_Garantias_DetalleLoteId",
                table: "Garantias",
                column: "DetalleLoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Documento_Id",
                table: "Documento",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documento_DetalleLoteModelo_Id",
                table: "Documento",
                column: "Id",
                principalTable: "DetalleLoteModelo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Garantias_Almacenes_AlmacenId",
                table: "Garantias",
                column: "AlmacenId",
                principalTable: "Almacenes",
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
    }
}
