using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChazuraProgram.Migrations
{
    public partial class LimudChartAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LimudChart",
                columns: table => new
                {
                    ChartId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    
                    UserId = table.Column<string>(maxLength: 450, nullable: false),
                    ChazurahType = table.Column<int>(nullable: false),
                    MeshctaCode = table.Column<string>(maxLength: 15, nullable: true),
                    DateStarted = table.Column<DateTime>(nullable: false),
                    EmailNotify = table.Column<bool>(nullable: false),
                    YearsChazurah = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LimudChart", x => x.ChartId);
                    table.ForeignKey(
                        name: "FK_LimudChart_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LimudChart_UserId",
                table: "LimudChart",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LimudChart");
        }
    }
}
