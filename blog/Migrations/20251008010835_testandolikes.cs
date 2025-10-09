using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog.Migrations
{
    /// <inheritdoc />
    public partial class testandolikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LikedByCurrentUser",
                table: "Post",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikedByCurrentUser",
                table: "Post");
        }
    }
}
