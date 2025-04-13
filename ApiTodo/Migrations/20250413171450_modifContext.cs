using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTodo.Migrations
{
    /// <inheritdoc />
    public partial class modifContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolesClaims_Roles_RoleId",
                table: "RolesClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UtilisateursClaims_Utilisateurs_UserId",
                table: "UtilisateursClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UtilisateursLogins_Utilisateurs_UserId",
                table: "UtilisateursLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UtilisateursRoles_Roles_RoleId",
                table: "UtilisateursRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UtilisateursRoles_Utilisateurs_UserId",
                table: "UtilisateursRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UtilisateursTokens_Utilisateurs_UserId",
                table: "UtilisateursTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UtilisateursTokens",
                table: "UtilisateursTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UtilisateursRoles",
                table: "UtilisateursRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UtilisateursLogins",
                table: "UtilisateursLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UtilisateursClaims",
                table: "UtilisateursClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolesClaims",
                table: "RolesClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "UtilisateursTokens",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "UtilisateursRoles",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "UtilisateursLogins",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "UtilisateursClaims",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "RolesClaims",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "AspNetRoles");

            migrationBuilder.RenameIndex(
                name: "IX_UtilisateursRoles_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UtilisateursLogins_UserId",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UtilisateursClaims_UserId",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RolesClaims_RoleId",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_Utilisateurs_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_Utilisateurs_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_Utilisateurs_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_Utilisateurs_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_Utilisateurs_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_Utilisateurs_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_Utilisateurs_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_Utilisateurs_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "UtilisateursTokens");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "UtilisateursRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "UtilisateursLogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "UtilisateursClaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "RolesClaims");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "UtilisateursRoles",
                newName: "IX_UtilisateursRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "UtilisateursLogins",
                newName: "IX_UtilisateursLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "UtilisateursClaims",
                newName: "IX_UtilisateursClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "RolesClaims",
                newName: "IX_RolesClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UtilisateursTokens",
                table: "UtilisateursTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UtilisateursRoles",
                table: "UtilisateursRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UtilisateursLogins",
                table: "UtilisateursLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UtilisateursClaims",
                table: "UtilisateursClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolesClaims",
                table: "RolesClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolesClaims_Roles_RoleId",
                table: "RolesClaims",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UtilisateursClaims_Utilisateurs_UserId",
                table: "UtilisateursClaims",
                column: "UserId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UtilisateursLogins_Utilisateurs_UserId",
                table: "UtilisateursLogins",
                column: "UserId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UtilisateursRoles_Roles_RoleId",
                table: "UtilisateursRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UtilisateursRoles_Utilisateurs_UserId",
                table: "UtilisateursRoles",
                column: "UserId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UtilisateursTokens_Utilisateurs_UserId",
                table: "UtilisateursTokens",
                column: "UserId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
