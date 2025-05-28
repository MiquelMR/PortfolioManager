using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class FA_ContextPropertiesAddedCorrectedAgainAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavorsDeflation",
                table: "assets");

            migrationBuilder.DropColumn(
                name: "FavorsInflation",
                table: "assets");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "assets",
                newName: "InflationHedge");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InflationHedge",
                table: "assets",
                newName: "Value");

            migrationBuilder.AddColumn<int>(
                name: "FavorsDeflation",
                table: "assets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FavorsInflation",
                table: "assets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
