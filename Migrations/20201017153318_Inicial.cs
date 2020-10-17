using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistroPrestamos.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Moras",
                columns: table => new
                {
                    MoraID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moras", x => x.MoraID);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    PersonaID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombres = table.Column<string>(nullable: true),
                    Balance = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.PersonaID);
                });

            migrationBuilder.CreateTable(
                name: "Prestamos",
                columns: table => new
                {
                    PrestamoID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Concepto = table.Column<string>(nullable: true),
                    Monto = table.Column<float>(nullable: false),
                    Balance = table.Column<float>(nullable: false),
                    PersonaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestamos", x => x.PrestamoID);
                    table.ForeignKey(
                        name: "FK_Prestamos_Personas_PersonaID",
                        column: x => x.PersonaID,
                        principalTable: "Personas",
                        principalColumn: "PersonaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MorasDetalle",
                columns: table => new
                {
                    MoraDetalleID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MoraID = table.Column<int>(nullable: false),
                    PrestamoID = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MorasDetalle", x => x.MoraDetalleID);
                    table.ForeignKey(
                        name: "FK_MorasDetalle_Moras_MoraID",
                        column: x => x.MoraID,
                        principalTable: "Moras",
                        principalColumn: "MoraID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MorasDetalle_Prestamos_PrestamoID",
                        column: x => x.PrestamoID,
                        principalTable: "Prestamos",
                        principalColumn: "PrestamoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MorasDetalle_MoraID",
                table: "MorasDetalle",
                column: "MoraID");

            migrationBuilder.CreateIndex(
                name: "IX_MorasDetalle_PrestamoID",
                table: "MorasDetalle",
                column: "PrestamoID");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_PersonaID",
                table: "Prestamos",
                column: "PersonaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MorasDetalle");

            migrationBuilder.DropTable(
                name: "Moras");

            migrationBuilder.DropTable(
                name: "Prestamos");

            migrationBuilder.DropTable(
                name: "Personas");
        }
    }
}
