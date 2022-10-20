using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelListing.Migrations
{
    public partial class AddedDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ad1ed17c-bcbd-4e35-8be1-e5817ba54e1c", "7bffaeb5-c5d8-4de4-9b4b-60da49f3d651", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b4cf6d08-efd4-4e95-89d6-3d38ffce67d0", "33b25c20-fb93-40d2-8e55-f1b6a87603a8", "Administartor", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad1ed17c-bcbd-4e35-8be1-e5817ba54e1c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4cf6d08-efd4-4e95-89d6-3d38ffce67d0");
        }
    }
}
