using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SanblasBackend.Migrations
{
    /// <inheritdoc />
    public partial class User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bautismos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Cedula = table.Column<int>(type: "integer", nullable: false),
                    PrimerApellido = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SegundoApellido = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NombreParroquia = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    FechaBautismo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AnnioBautismo = table.Column<int>(type: "integer", nullable: false),
                    Prebispero = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HoraNacimiento = table.Column<TimeSpan>(type: "interval", nullable: false),
                    NombreAbuelosPaternos = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    NombreAbuelosMaternos = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bautismos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comuniones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DiaComunion = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    MesComunion = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    AnnioComunion = table.Column<int>(type: "integer", nullable: false),
                    LugarComunion = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comuniones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Confirmaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DiaConfirmacion = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    MesConfirmacion = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    AnnioConfirmacion = table.Column<int>(type: "integer", nullable: false),
                    LugarConfirmacion = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Confirmaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matrimonios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreContrayente = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NombreContrayente2 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DiaMatrimonio = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    MesMatrimonio = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    AnnioMatrimonio = table.Column<int>(type: "integer", nullable: false),
                    LugarMatrimonio = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Tomo = table.Column<int>(type: "integer", nullable: false),
                    Folio = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matrimonios", x => x.Id);
                });

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    UserRole = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    State = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bautismos");

            migrationBuilder.DropTable(
                name: "Comuniones");

            migrationBuilder.DropTable(
                name: "Confirmaciones");

            migrationBuilder.DropTable(
                name: "Matrimonios");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
