using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnicalSupportService.Migrations
{
    /// <inheritdoc />
    public partial class AddSparePartId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SparePartId",
                table: "interventions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SparePartId",
                table: "interventions");
        }
    }
}
