using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bieren.DataLayer.Migrations
{
    public partial class Initieel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbBrouwer",
                columns: table => new
                {
                    BrouwerNr = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrNaam = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Adres = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PostCode = table.Column<short>(type: "smallint", nullable: true),
                    Gemeente = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Omzet = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brouwers", x => x.BrouwerNr);
                });

            migrationBuilder.CreateTable(
                name: "DbSoort",
                columns: table => new
                {
                    SoortNr = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Soort = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Soorten", x => x.SoortNr);
                });

            migrationBuilder.CreateTable(
                name: "DbUser",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Voornaam = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Familienaam = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    GeboorteDatum = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Email = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "DbBier",
                columns: table => new
                {
                    BierNr = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    BrouwerNr = table.Column<int>(type: "int", nullable: true),
                    SoortNr = table.Column<int>(type: "int", nullable: true),
                    Alcohol = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bieren", x => x.BierNr);
                    table.ForeignKey(
                        name: "FK_Bieren_Brouwers",
                        column: x => x.BrouwerNr,
                        principalTable: "DbBrouwer",
                        principalColumn: "BrouwerNr",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bieren_Soorten",
                        column: x => x.SoortNr,
                        principalTable: "DbSoort",
                        principalColumn: "SoortNr",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DbBierDbUser",
                columns: table => new
                {
                    FavorieteBierenBierNr = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbBierDbUser", x => new { x.FavorieteBierenBierNr, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_DbBierDbUser_DbBier_FavorieteBierenBierNr",
                        column: x => x.FavorieteBierenBierNr,
                        principalTable: "DbBier",
                        principalColumn: "BierNr",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbBierDbUser_DbUser_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "DbUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbBier_BrouwerNr",
                table: "DbBier",
                column: "BrouwerNr");

            migrationBuilder.CreateIndex(
                name: "IX_DbBier_SoortNr",
                table: "DbBier",
                column: "SoortNr");

            migrationBuilder.CreateIndex(
                name: "IX_DbBierDbUser_UsersUserId",
                table: "DbBierDbUser",
                column: "UsersUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbBierDbUser");

            migrationBuilder.DropTable(
                name: "DbBier");

            migrationBuilder.DropTable(
                name: "DbUser");

            migrationBuilder.DropTable(
                name: "DbBrouwer");

            migrationBuilder.DropTable(
                name: "DbSoort");
        }
    }
}
