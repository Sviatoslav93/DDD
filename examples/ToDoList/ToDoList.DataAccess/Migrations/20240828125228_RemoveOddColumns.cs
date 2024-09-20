using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOddColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_ToDoLists_ToDoListId",
                table: "ToDoItems");

            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "ToDoItems");

            migrationBuilder.DropColumn(
                name: "IsFailed",
                table: "ToDoItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "ToDoListId",
                table: "ToDoItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_ToDoLists_ToDoListId",
                table: "ToDoItems",
                column: "ToDoListId",
                principalTable: "ToDoLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_ToDoLists_ToDoListId",
                table: "ToDoItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "ToDoListId",
                table: "ToDoItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "ToDoItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFailed",
                table: "ToDoItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_ToDoLists_ToDoListId",
                table: "ToDoItems",
                column: "ToDoListId",
                principalTable: "ToDoLists",
                principalColumn: "Id");
        }
    }
}
