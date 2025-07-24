using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace anaproject.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCarChargeFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ACChargeTimeFast",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ACChargeTimeSlow",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "BatteryCapacity",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "DCChargeTime",
                table: "Cars");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ACChargeTimeFast",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ACChargeTimeSlow",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BatteryCapacity",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DCChargeTime",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
