using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpoApp.Repository.Migrations
{
    public partial class Location_Nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expos_Location_LocationId",
                table: "Expos");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Expos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Expos_Location_LocationId",
                table: "Expos",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expos_Location_LocationId",
                table: "Expos");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Expos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Expos_Location_LocationId",
                table: "Expos",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
