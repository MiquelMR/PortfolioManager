using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class FA_ContextPropertiesAddedCorrected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Volatilidad",
                table: "assets",
                newName: "VolatilyLevel");

            migrationBuilder.RenameColumn(
                name: "GrowthValue",
                table: "assets",
                newName: "GrowthVsValue");

            migrationBuilder.RenameColumn(
                name: "EntornoInflacion",
                table: "assets",
                newName: "FavorsInflation");

            migrationBuilder.RenameColumn(
                name: "EntornoCrecimiento",
                table: "assets",
                newName: "FavorsGrowth");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VolatilyLevel",
                table: "assets",
                newName: "Volatilidad");

            migrationBuilder.RenameColumn(
                name: "GrowthVsValue",
                table: "assets",
                newName: "GrowthValue");

            migrationBuilder.RenameColumn(
                name: "FavorsInflation",
                table: "assets",
                newName: "EntornoInflacion");

            migrationBuilder.RenameColumn(
                name: "FavorsGrowth",
                table: "assets",
                newName: "EntornoCrecimiento");
        }
    }
}
