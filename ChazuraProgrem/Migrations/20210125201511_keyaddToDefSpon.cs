using Microsoft.EntityFrameworkCore.Migrations;

namespace ChazuraProgram.Migrations
{
    public partial class keyaddToDefSpon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultId",
                table: "DefaultSponsor",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DefaultSponsor",
                table: "DefaultSponsor",
                column: "DefaultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DefaultSponsor",
                table: "DefaultSponsor");

            migrationBuilder.DropColumn(
                name: "DefaultId",
                table: "DefaultSponsor");
        }
    }
}
