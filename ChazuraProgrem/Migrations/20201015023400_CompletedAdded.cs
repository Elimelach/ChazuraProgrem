using Microsoft.EntityFrameworkCore.Migrations;

namespace ChazuraProgram.Migrations
{
    public partial class CompletedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_LimudChart_AspNetUsers_UserId",
            //    table: "LimudChart");

            //migrationBuilder.DropColumn(
            //    name: "Id",
            //    table: "LimudChart");

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserId",
            //    table: "LimudChart",
            //    maxLength: 450,
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)",
            //    oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Completeds",
                columns: table => new
                {
                    CompId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LimudChartId = table.Column<int>(nullable: false),
                    LimudFinishedCode = table.Column<string>(nullable: true),
                    ChazuraTimes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Completeds", x => x.CompId);
                    table.ForeignKey(
                        name: "FK_Completeds_LimudChart_LimudChartId",
                        column: x => x.LimudChartId,
                        principalTable: "LimudChart",
                        principalColumn: "ChartId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Completeds_LimudChartId",
                table: "Completeds",
                column: "LimudChartId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_LimudChart_AspNetUsers_UserId",
            //    table: "LimudChart",
            //    column: "UserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_LimudChart_AspNetUsers_UserId",
            //    table: "LimudChart");

            migrationBuilder.DropTable(
                name: "Completeds");

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserId",
            //    table: "LimudChart",
            //    type: "nvarchar(450)",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldMaxLength: 450);

            //migrationBuilder.AddColumn<string>(
            //    name: "Id",
            //    table: "LimudChart",
            //    type: "nvarchar(450)",
            //    maxLength: 450,
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_LimudChart_AspNetUsers_UserId",
            //    table: "LimudChart",
            //    column: "UserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
