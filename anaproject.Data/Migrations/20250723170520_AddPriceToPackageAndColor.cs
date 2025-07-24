using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace anaproject.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToPackageAndColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PackagePrice",
                table: "Packages",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ColorPrice",
                table: "Colors",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackagePrice",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "ColorPrice",
                table: "Colors");
        }
    }
}
