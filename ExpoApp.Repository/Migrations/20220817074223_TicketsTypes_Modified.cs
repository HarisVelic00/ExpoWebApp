using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpoApp.Repository.Migrations
{
    public partial class TicketsTypes_Modified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpoId",
                table: "TicketType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "TicketType",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ValidDaysCount",
                table: "TicketType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TicketType_ExpoId",
                table: "TicketType",
                column: "ExpoId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketType_Expos_ExpoId",
                table: "TicketType",
                column: "ExpoId",
                principalTable: "Expos",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketType_Expos_ExpoId",
                table: "TicketType");

            migrationBuilder.DropIndex(
                name: "IX_TicketType_ExpoId",
                table: "TicketType");

            migrationBuilder.DropColumn(
                name: "ExpoId",
                table: "TicketType");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "TicketType");

            migrationBuilder.DropColumn(
                name: "ValidDaysCount",
                table: "TicketType");
        }
    }
}
