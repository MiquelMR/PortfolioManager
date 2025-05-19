using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class portfolio_author_renamed_to_owner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Portfolios",
                newName: "Owner");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Owner",
                table: "Portfolios",
                newName: "Author");
        }
    }
}
