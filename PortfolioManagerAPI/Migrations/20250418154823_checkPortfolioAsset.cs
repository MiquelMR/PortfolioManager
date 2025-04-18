using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class checkPortfolioAsset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PortfolioAssets_AssetId",
                table: "PortfolioAssets",
                column: "AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioAssets_assets_AssetId",
                table: "PortfolioAssets",
                column: "AssetId",
                principalTable: "assets",
                principalColumn: "AssetId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioAssets_assets_AssetId",
                table: "PortfolioAssets");

            migrationBuilder.DropIndex(
                name: "IX_PortfolioAssets_AssetId",
                table: "PortfolioAssets");
        }
    }
}
