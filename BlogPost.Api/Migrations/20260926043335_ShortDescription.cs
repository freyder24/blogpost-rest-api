using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogPostApi.Migrations
{
    /// <inheritdoc />
    public partial class ShortDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Excerpt",
                table: "BlogPosts",
                newName: "ShortDescription");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortDescription",
                table: "BlogPosts",
                newName: "Excerpt");
        }
    }
}
