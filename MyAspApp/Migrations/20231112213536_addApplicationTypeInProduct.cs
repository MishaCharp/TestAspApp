using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAspApp.Migrations
{
    /// <inheritdoc />
    public partial class addApplicationTypeInProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Product");
        }
    }
}
