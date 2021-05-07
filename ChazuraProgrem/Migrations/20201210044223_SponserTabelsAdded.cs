using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChazuraProgram.Migrations
{
    public partial class SponserTabelsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateEnterd = table.Column<DateTime>(nullable: false),
                    DateCharged = table.Column<DateTime>(nullable: true),
                    CC_Number = table.Column<string>(nullable: false),
                    CardType = table.Column<int>(nullable: false),
                    ZipCode = table.Column<string>(nullable: false),
                    ExpDate = table.Column<string>(nullable: false),
                    BCN = table.Column<string>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    GetStatus = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sponsors",
                columns: table => new
                {
                    SponsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    GetSponserType = table.Column<int>(nullable: false),
                    DescriptionName = table.Column<string>(nullable: false),
                    DescriptionElse = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    PaymentId = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponsors", x => x.SponsId);
                    table.ForeignKey(
                        name: "FK_Sponsors_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sponsors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                table: "Payments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sponsors_PaymentId",
                table: "Sponsors",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Sponsors_UserId",
                table: "Sponsors",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sponsors");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "State",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "AspNetUsers");
        }
    }
}
