using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnicalSupportService.Migrations
{
    /// <inheritdoc />
    public partial class interventionProductPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductPhoto",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductPhoto",
                table: "Products");
        }
    }
}
