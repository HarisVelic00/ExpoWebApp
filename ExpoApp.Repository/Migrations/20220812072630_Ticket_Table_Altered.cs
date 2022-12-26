using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpoApp.Repository.Migrations
{
    public partial class Ticket_Table_Altered : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expired",
                table: "Ticket");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Ticket",
                newName: "DateTo");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateFrom",
                table: "Ticket",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateFrom",
                table: "Ticket");

            migrationBuilder.RenameColumn(
                name: "DateTo",
                table: "Ticket",
                newName: "Date");

            migrationBuilder.AddColumn<bool>(
                name: "Expired",
                table: "Ticket",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
