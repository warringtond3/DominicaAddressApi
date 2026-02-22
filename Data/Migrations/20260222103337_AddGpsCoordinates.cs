using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DominicaAddressAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGpsCoordinates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Streets",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Streets",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Settlements",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Settlements",
                type: "REAL",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Streets");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Streets");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Settlements");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Settlements");
        }
    }
}
