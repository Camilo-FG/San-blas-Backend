using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SanblasBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddObservacionEstadoInscripcionCatequesis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActualizacionEstado",
                table: "InscripcionesCatequesis",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ObservacionAdministrativa",
                table: "InscripcionesCatequesis",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaActualizacionEstado",
                table: "InscripcionesCatequesis");

            migrationBuilder.DropColumn(
                name: "ObservacionAdministrativa",
                table: "InscripcionesCatequesis");
        }
    }
}
