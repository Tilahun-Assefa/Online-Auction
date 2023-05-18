using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineAuction.Migrations
{
    /// <inheritdoc />
    public partial class productedited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "User",
                table: "Reviews",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Products",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Reviews",
                newName: "User");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "Title");

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Products",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
