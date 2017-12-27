using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FindingImmo.Core.Migrations
{
    public partial class AddFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "caracteristiques",
                columns: table => new
                {
                    id_offre = table.Column<long>(nullable: false),
                    bifamille = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_caracteristiques", x => x.id_offre);
                    table.ForeignKey(
                        name: "FK_caracteristiques_offre_id_offre",
                        column: x => x.id_offre,
                        principalTable: "offre",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "caracteristiques");
        }
    }
}
