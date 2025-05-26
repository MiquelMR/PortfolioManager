using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class FA_ContextPropertiesAddedCorrectedAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VolatilyLevel",
                table: "assets",
                newName: "Volatility");

            migrationBuilder.RenameColumn(
                name: "GrowthVsValue",
                table: "assets",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "FavorsGrowth",
                table: "assets",
                newName: "Income");

            migrationBuilder.AddColumn<int>(
                name: "Defensive",
                table: "assets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FavorsDeflation",
                table: "assets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FavorsExpansion",
                table: "assets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Growth",
                table: "assets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Defensive",
                table: "assets");

            migrationBuilder.DropColumn(
                name: "FavorsDeflation",
                table: "assets");

            migrationBuilder.DropColumn(
                name: "FavorsExpansion",
                table: "assets");

            migrationBuilder.DropColumn(
                name: "Growth",
                table: "assets");

            migrationBuilder.RenameColumn(
                name: "Volatility",
                table: "assets",
                newName: "VolatilyLevel");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "assets",
                newName: "GrowthVsValue");

            migrationBuilder.RenameColumn(
                name: "Income",
                table: "assets",
                newName: "FavorsGrowth");
        }
    }
}
