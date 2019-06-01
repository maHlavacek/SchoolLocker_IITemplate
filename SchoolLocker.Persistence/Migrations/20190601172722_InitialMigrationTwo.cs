using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolLocker.Persistence.Migrations
{
    public partial class InitialMigrationTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Pupils",
                newName: "Version");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Lockers",
                newName: "Version");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Bookings",
                newName: "Version");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Version",
                table: "Pupils",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "Version",
                table: "Lockers",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "Version",
                table: "Bookings",
                newName: "Timestamp");
        }
    }
}
