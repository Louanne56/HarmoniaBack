using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTodo.Migrations
{
    /// <inheritdoc />
    public partial class AttributAudio2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Position2",
                table: "Accords",
                newName: "Diagram2");

            migrationBuilder.RenameColumn(
                name: "Position1",
                table: "Accords",
                newName: "Diagram1");

            migrationBuilder.AddColumn<string>(
                name: "Audio2",
                table: "Accords",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Audio2",
                table: "Accords");

            migrationBuilder.RenameColumn(
                name: "Diagram2",
                table: "Accords",
                newName: "Position2");

            migrationBuilder.RenameColumn(
                name: "Diagram1",
                table: "Accords",
                newName: "Position1");
        }
    }
}
