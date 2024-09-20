using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ToDoItemTitleUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_Title_ToDoListId",
                table: "ToDoItems",
                columns: new[] { "Title", "ToDoListId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ToDoItems_Title_ToDoListId",
                table: "ToDoItems");
        }
    }
}
