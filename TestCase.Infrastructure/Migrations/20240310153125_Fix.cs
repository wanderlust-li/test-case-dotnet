using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestCase.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClientLocationLongitude",
                table: "Transactions",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "ClientLocationLatitude",
                table: "Transactions",
                newName: "Latitude");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "Transactions",
                newName: "ClientLocationLongitude");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "Transactions",
                newName: "ClientLocationLatitude");
        }
    }
}
