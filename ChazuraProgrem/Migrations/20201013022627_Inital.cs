using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChazuraProgram.Migrations
{
    public partial class Inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MeshctaShas",
                columns: table => new
                {
                    MeshactaID = table.Column<string>(maxLength: 10, nullable: false),
                    MeshactaHebrawName = table.Column<string>(maxLength: 15, nullable: false),
                    MeshachtaEngName = table.Column<string>(maxLength: 15, nullable: false),
                    TotolDafim = table.Column<int>(nullable: false),
                    StartsAtDaf = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeshctaShas", x => x.MeshactaID);
                });

            migrationBuilder.CreateTable(
                name: "DafimShas",
                columns: table => new
                {
                    DafID = table.Column<string>(maxLength: 14, nullable: false),
                    TwoSided = table.Column<bool>(nullable: false),
                    DafHebraw = table.Column<string>(maxLength: 3, nullable: false),
                    DafNumber = table.Column<int>(nullable: false),
                    MeshactaID = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DafimShas", x => x.DafID);
                    table.ForeignKey(
                        name: "FK_DafimShas_MeshctaShas_MeshactaID",
                        column: x => x.MeshactaID,
                        principalTable: "MeshctaShas",
                        principalColumn: "MeshactaID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ShasChazuraData",
                columns: table => new
                {
                    ChartId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DafID = table.Column<string>(maxLength: 14, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    MeshactaID = table.Column<string>(maxLength: 10, nullable: false),
                    ChazuraTimes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShasChazuraData", x => x.ChartId);
                    table.ForeignKey(
                        name: "FK_ShasChazuraData_DafimShas_DafID",
                        column: x => x.DafID,
                        principalTable: "DafimShas",
                        principalColumn: "DafID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShasChazuraData_MeshctaShas_MeshactaID",
                        column: x => x.MeshactaID,
                        principalTable: "MeshctaShas",
                        principalColumn: "MeshactaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DafimShas_MeshactaID",
                table: "DafimShas",
                column: "MeshactaID");

            migrationBuilder.CreateIndex(
                name: "IX_ShasChazuraData_DafID",
                table: "ShasChazuraData",
                column: "DafID");

            migrationBuilder.CreateIndex(
                name: "IX_ShasChazuraData_MeshactaID",
                table: "ShasChazuraData",
                column: "MeshactaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShasChazuraData");

            migrationBuilder.DropTable(
                name: "DafimShas");

            migrationBuilder.DropTable(
                name: "MeshctaShas");
        }
    }
}
