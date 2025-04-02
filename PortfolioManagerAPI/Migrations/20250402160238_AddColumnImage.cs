using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioAssets_assets_AssetId",
                table: "PortfolioAssets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_PortfolioAssets_AssetId",
                table: "PortfolioAssets");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "assets",
                type: "varchar(48)",
                maxLength: 48,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "assets");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

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
    }
}
