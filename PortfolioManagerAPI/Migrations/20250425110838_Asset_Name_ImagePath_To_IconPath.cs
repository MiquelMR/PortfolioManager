using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class Asset_Name_ImagePath_To_IconPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "assets",
                newName: "IconPath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IconPath",
                table: "assets",
                newName: "ImagePath");
        }
    }
}
