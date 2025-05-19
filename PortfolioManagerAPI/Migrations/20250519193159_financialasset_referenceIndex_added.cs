using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class financialasset_referenceIndex_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReferenceIndex",
                table: "assets",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferenceIndex",
                table: "assets");
        }
    }
}
