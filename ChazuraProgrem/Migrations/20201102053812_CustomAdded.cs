using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChazuraProgram.Migrations
{
    public partial class CustomAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_LimudChart_AspNetUsers_UserId",
            //    table: "LimudChart");

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserId",
            //    table: "LimudChart",
            //    maxLength: 450,
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)",
            //    oldMaxLength: 450);

            migrationBuilder.CreateTable(
                name: "Custom",
                columns: table => new
                {
                    CustomID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    ChazuraTimes = table.Column<int>(nullable: false),
                    LimudString = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Completed = table.Column<bool>(nullable: false),
                    EmailNotify = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Custom", x => x.CustomID);
                    table.ForeignKey(
                        name: "FK_Custom_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Custom_UserId",
                table: "Custom",
                column: "UserId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_LimudChart_AspNetUsers_UserId",
            //    table: "LimudChart",
            //    column: "UserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_LimudChart_AspNetUsers_UserId",
            //    table: "LimudChart");

            migrationBuilder.DropTable(
                name: "Custom");

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserId",
            //    table: "LimudChart",
            //    type: "nvarchar(450)",
            //    maxLength: 450,
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldMaxLength: 450,
            //    oldNullable: true);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_LimudChart_AspNetUsers_UserId",
            //    table: "LimudChart",
            //    column: "UserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
