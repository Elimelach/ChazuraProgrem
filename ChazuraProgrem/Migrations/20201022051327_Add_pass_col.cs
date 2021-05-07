using Microsoft.EntityFrameworkCore.Migrations;

namespace ChazuraProgram.Migrations
{
    public partial class Add_pass_col : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LimudChart_AspNetUsers_UserId",
                table: "LimudChart");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LimudChart",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "LimudChart",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SavedPassword",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LimudChart_AspNetUsers_UserId",
                table: "LimudChart",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LimudChart_AspNetUsers_UserId",
                table: "LimudChart");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "LimudChart");

            migrationBuilder.DropColumn(
                name: "SavedPassword",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LimudChart",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LimudChart_AspNetUsers_UserId",
                table: "LimudChart",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
