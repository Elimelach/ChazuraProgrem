using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChazuraProgram.Migrations
{
    public partial class AmidumAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shas1Sided",
                columns: table => new
                {
                    AumidID = table.Column<string>(maxLength: 14, nullable: false),
                    AumidHebraw = table.Column<string>(maxLength: 3, nullable: false),
                    DafNumber = table.Column<int>(nullable: false),
                    AumidNumber = table.Column<int>(nullable: false),
                    MeshactaID = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shas1Sided", x => x.AumidID);
                    table.ForeignKey(
                        name: "FK_Shas1Sided_MeshctaShas_MeshactaID",
                        column: x => x.MeshactaID,
                        principalTable: "MeshctaShas",
                        principalColumn: "MeshactaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShasChazuraAumidData",
                columns: table => new
                {
                    ChartId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AumidID = table.Column<string>(maxLength: 14, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    MeshactaID = table.Column<string>(maxLength: 10, nullable: false),
                    ChazuraTimes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShasChazuraAumidData", x => x.ChartId);
                    table.ForeignKey(
                        name: "FK_ShasChazuraAumidData_Shas1Sided_AumidID",
                        column: x => x.AumidID,
                        principalTable: "Shas1Sided",
                        principalColumn: "AumidID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShasChazuraAumidData_MeshctaShas_MeshactaID",
                        column: x => x.MeshactaID,
                        principalTable: "MeshctaShas",
                        principalColumn: "MeshactaID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shas1Sided_MeshactaID",
                table: "Shas1Sided",
                column: "MeshactaID");

            migrationBuilder.CreateIndex(
                name: "IX_ShasChazuraAumidData_AumidID",
                table: "ShasChazuraAumidData",
                column: "AumidID");

            migrationBuilder.CreateIndex(
                name: "IX_ShasChazuraAumidData_MeshactaID",
                table: "ShasChazuraAumidData",
                column: "MeshactaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShasChazuraAumidData");

            migrationBuilder.DropTable(
                name: "Shas1Sided");
        }
    }
}
