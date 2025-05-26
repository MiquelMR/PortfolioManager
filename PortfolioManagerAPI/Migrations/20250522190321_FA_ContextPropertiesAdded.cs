using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class FA_ContextPropertiesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EntornoCrecimiento",
                table: "assets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EntornoInflacion",
                table: "assets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GrowthValue",
                table: "assets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Volatilidad",
                table: "assets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntornoCrecimiento",
                table: "assets");

            migrationBuilder.DropColumn(
                name: "EntornoInflacion",
                table: "assets");

            migrationBuilder.DropColumn(
                name: "GrowthValue",
                table: "assets");

            migrationBuilder.DropColumn(
                name: "Volatilidad",
                table: "assets");
        }
    }
}
