using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTodo.Migrations
{
    /// <inheritdoc />
    public partial class reModifContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accords_ProgressionAccords_ProgressionAccordsId",
                table: "Accords");

            migrationBuilder.DropIndex(
                name: "IX_Accords_ProgressionAccordsId",
                table: "Accords");

            migrationBuilder.DropColumn(
                name: "ProgressionAccordsId",
                table: "Accords");

            migrationBuilder.CreateTable(
                name: "ProgressionAccord",
                columns: table => new
                {
                    AccordsId = table.Column<string>(type: "TEXT", nullable: false),
                    ProgressionsId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressionAccord", x => new { x.AccordsId, x.ProgressionsId });
                    table.ForeignKey(
                        name: "FK_ProgressionAccord_Accords_AccordsId",
                        column: x => x.AccordsId,
                        principalTable: "Accords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgressionAccord_ProgressionAccords_ProgressionsId",
                        column: x => x.ProgressionsId,
                        principalTable: "ProgressionAccords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgressionAccord_ProgressionsId",
                table: "ProgressionAccord",
                column: "ProgressionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgressionAccord");

            migrationBuilder.AddColumn<string>(
                name: "ProgressionAccordsId",
                table: "Accords",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accords_ProgressionAccordsId",
                table: "Accords",
                column: "ProgressionAccordsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accords_ProgressionAccords_ProgressionAccordsId",
                table: "Accords",
                column: "ProgressionAccordsId",
                principalTable: "ProgressionAccords",
                principalColumn: "Id");
        }
    }
}
