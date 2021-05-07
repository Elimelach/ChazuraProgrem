using Microsoft.EntityFrameworkCore.Migrations;

namespace ChazuraProgram.Migrations
{
    public partial class changeLimudChart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LimudChart_AspNetUsers_UserId",
                table: "LimudChart");

            //migrationBuilder.DropColumn(
            //    name: "UserId",
            //    table: "LimudChart");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LimudChart",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LimudChart_AspNetUsers_UserId",
                table: "LimudChart",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LimudChart_AspNetUsers_UserId",
                table: "LimudChart");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LimudChart",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 450);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "LimudChart",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_LimudChart_AspNetUsers_UserId",
                table: "LimudChart",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
