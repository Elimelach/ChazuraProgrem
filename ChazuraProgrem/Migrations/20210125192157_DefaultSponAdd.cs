using Microsoft.EntityFrameworkCore.Migrations;

namespace ChazuraProgram.Migrations
{
    public partial class DefaultSponAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DefaultSponsor",
                columns: table => new
                {
                    GetSponserType = table.Column<int>(type: "int", nullable: false),
                    DescriptionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionElse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultSponsor");
        }
    }
}
