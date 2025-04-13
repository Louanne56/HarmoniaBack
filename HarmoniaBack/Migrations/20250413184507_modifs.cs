using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HarmoniaBack.Migrations
{
    /// <inheritdoc />
    public partial class modifs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accords",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Diagram1 = table.Column<string>(type: "TEXT", nullable: false),
                    Diagram2 = table.Column<string>(type: "TEXT", nullable: true),
                    Audio = table.Column<string>(type: "TEXT", nullable: false),
                    Audio2 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgressionAccords",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Mode = table.Column<int>(type: "INTEGER", nullable: false),
                    Style = table.Column<int>(type: "INTEGER", nullable: false),
                    Tonalite = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressionAccords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    RefreshToken = table.Column<string>(type: "TEXT", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "SuitesFavorites",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ProgressionAccordsId = table.Column<string>(type: "TEXT", nullable: false),
                    UtilisateurId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuitesFavorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuitesFavorites_ProgressionAccords_ProgressionAccordsId",
                        column: x => x.ProgressionAccordsId,
                        principalTable: "ProgressionAccords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SuitesFavorites_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgressionAccord_ProgressionsId",
                table: "ProgressionAccord",
                column: "ProgressionsId");

            migrationBuilder.CreateIndex(
                name: "IX_SuitesFavorites_ProgressionAccordsId",
                table: "SuitesFavorites",
                column: "ProgressionAccordsId");

            migrationBuilder.CreateIndex(
                name: "IX_SuitesFavorites_UtilisateurId",
                table: "SuitesFavorites",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Utilisateurs",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Utilisateurs",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgressionAccord");

            migrationBuilder.DropTable(
                name: "SuitesFavorites");

            migrationBuilder.DropTable(
                name: "Accords");

            migrationBuilder.DropTable(
                name: "ProgressionAccords");

            migrationBuilder.DropTable(
                name: "Utilisateurs");
        }
    }
}
