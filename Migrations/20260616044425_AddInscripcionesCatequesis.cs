using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SanblasBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddInscripcionesCatequesis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TipoSacramento",
                table: "FormSacras",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "InscripcionesCatequesis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CentroCatequesis = table.Column<string>(type: "text", nullable: false),
                    NivelAInscribirse = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false, defaultValue: "Pendiente"),
                    FechaSolicitud = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InscripcionesCatequesis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdecuacionesCatequizando",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InscripcionCatequesisId = table.Column<int>(type: "integer", nullable: false),
                    RequiereAdecuacionCentroEducativo = table.Column<bool>(type: "boolean", nullable: true),
                    DescripcionAdecuacion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdecuacionesCatequizando", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdecuacionesCatequizando_InscripcionesCatequesis_Inscripcio~",
                        column: x => x.InscripcionCatequesisId,
                        principalTable: "InscripcionesCatequesis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BautismosCatequizando",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InscripcionCatequesisId = table.Column<int>(type: "integer", nullable: false),
                    Parroquia = table.Column<string>(type: "text", nullable: true),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: true),
                    Tomo = table.Column<string>(type: "text", nullable: true),
                    Folio = table.Column<string>(type: "text", nullable: true),
                    Asiento = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BautismosCatequizando", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BautismosCatequizando_InscripcionesCatequesis_InscripcionCa~",
                        column: x => x.InscripcionCatequesisId,
                        principalTable: "InscripcionesCatequesis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Catequizandos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InscripcionCatequesisId = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Apellidos = table.Column<string>(type: "text", nullable: false),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    DireccionExacta = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catequizandos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catequizandos_InscripcionesCatequesis_InscripcionCatequesis~",
                        column: x => x.InscripcionCatequesisId,
                        principalTable: "InscripcionesCatequesis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CondicionesSaludCatequizando",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InscripcionCatequesisId = table.Column<int>(type: "integer", nullable: false),
                    PortadorEnfermedadCronica = table.Column<bool>(type: "boolean", nullable: true),
                    DescripcionEnfermedad = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CondicionesSaludCatequizando", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CondicionesSaludCatequizando_InscripcionesCatequesis_Inscri~",
                        column: x => x.InscripcionCatequesisId,
                        principalTable: "InscripcionesCatequesis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MadresCatequizando",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InscripcionCatequesisId = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Apellidos = table.Column<string>(type: "text", nullable: false),
                    DireccionExacta = table.Column<string>(type: "text", nullable: true),
                    Ciudad = table.Column<string>(type: "text", nullable: true),
                    Provincia = table.Column<string>(type: "text", nullable: true),
                    Telefono = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MadresCatequizando", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MadresCatequizando_InscripcionesCatequesis_InscripcionCateq~",
                        column: x => x.InscripcionCatequesisId,
                        principalTable: "InscripcionesCatequesis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdecuacionesCatequizando_InscripcionCatequesisId",
                table: "AdecuacionesCatequizando",
                column: "InscripcionCatequesisId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BautismosCatequizando_InscripcionCatequesisId",
                table: "BautismosCatequizando",
                column: "InscripcionCatequesisId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Catequizandos_InscripcionCatequesisId",
                table: "Catequizandos",
                column: "InscripcionCatequesisId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CondicionesSaludCatequizando_InscripcionCatequesisId",
                table: "CondicionesSaludCatequizando",
                column: "InscripcionCatequesisId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MadresCatequizando_InscripcionCatequesisId",
                table: "MadresCatequizando",
                column: "InscripcionCatequesisId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdecuacionesCatequizando");

            migrationBuilder.DropTable(
                name: "BautismosCatequizando");

            migrationBuilder.DropTable(
                name: "Catequizandos");

            migrationBuilder.DropTable(
                name: "CondicionesSaludCatequizando");

            migrationBuilder.DropTable(
                name: "MadresCatequizando");

            migrationBuilder.DropTable(
                name: "InscripcionesCatequesis");

            migrationBuilder.AlterColumn<int>(
                name: "TipoSacramento",
                table: "FormSacras",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
