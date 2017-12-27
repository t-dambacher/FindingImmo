using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FindingImmo.Core.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "offre",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    date_creation = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    classe_energie = table.Column<int>(nullable: false),
                    reference_externe = table.Column<string>(nullable: false),
                    GES = table.Column<int>(nullable: false),
                    est_agence = table.Column<bool>(nullable: false),
                    date_derniere_maj = table.Column<DateTime>(nullable: false),
                    site_origine = table.Column<int>(nullable: false),
                    code_postal = table.Column<string>(maxLength: 5, nullable: false),
                    prix = table.Column<int>(nullable: false),
                    nombre_pieces = table.Column<int>(nullable: false),
                    surface = table.Column<int>(nullable: false),
                    titre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_offre", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "image",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_offre = table.Column<long>(nullable: false),
                    hauteur = table.Column<int>(nullable: false),
                    url = table.Column<string>(nullable: false),
                    largeur = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_image", x => x.id);
                    table.ForeignKey(
                        name: "FK_image_offre_id_offre",
                        column: x => x.id_offre,
                        principalTable: "offre",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_image_id_offre",
                table: "image",
                column: "id_offre");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "image");

            migrationBuilder.DropTable(
                name: "offre");
        }
    }
}
