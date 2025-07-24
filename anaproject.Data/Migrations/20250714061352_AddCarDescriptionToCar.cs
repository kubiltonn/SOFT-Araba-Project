using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace anaproject.Migrations
{
    /// <inheritdoc />
    public partial class AddCarDescriptionToCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarDescription",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarDescription",
                table: "Cars");
        }
    }
}
