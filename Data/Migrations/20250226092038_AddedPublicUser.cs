using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace discgolf_duels.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedPublicUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitions_PublicUser_PublicUserId",
                table: "Competitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Playing_PublicUser_PublicUserId",
                table: "Playing");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicUser_AspNetUsers_Email",
                table: "PublicUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_PublicUser_PublicUserId",
                table: "Registrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PublicUser",
                table: "PublicUser");

            migrationBuilder.RenameTable(
                name: "PublicUser",
                newName: "PublicUsers");

            migrationBuilder.RenameIndex(
                name: "IX_PublicUser_Email",
                table: "PublicUsers",
                newName: "IX_PublicUsers_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PublicUsers",
                table: "PublicUsers",
                column: "PublicUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitions_PublicUsers_PublicUserId",
                table: "Competitions",
                column: "PublicUserId",
                principalTable: "PublicUsers",
                principalColumn: "PublicUserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Playing_PublicUsers_PublicUserId",
                table: "Playing",
                column: "PublicUserId",
                principalTable: "PublicUsers",
                principalColumn: "PublicUserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicUsers_AspNetUsers_Email",
                table: "PublicUsers",
                column: "Email",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_PublicUsers_PublicUserId",
                table: "Registrations",
                column: "PublicUserId",
                principalTable: "PublicUsers",
                principalColumn: "PublicUserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitions_PublicUsers_PublicUserId",
                table: "Competitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Playing_PublicUsers_PublicUserId",
                table: "Playing");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicUsers_AspNetUsers_Email",
                table: "PublicUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_PublicUsers_PublicUserId",
                table: "Registrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PublicUsers",
                table: "PublicUsers");

            migrationBuilder.RenameTable(
                name: "PublicUsers",
                newName: "PublicUser");

            migrationBuilder.RenameIndex(
                name: "IX_PublicUsers_Email",
                table: "PublicUser",
                newName: "IX_PublicUser_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PublicUser",
                table: "PublicUser",
                column: "PublicUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitions_PublicUser_PublicUserId",
                table: "Competitions",
                column: "PublicUserId",
                principalTable: "PublicUser",
                principalColumn: "PublicUserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Playing_PublicUser_PublicUserId",
                table: "Playing",
                column: "PublicUserId",
                principalTable: "PublicUser",
                principalColumn: "PublicUserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicUser_AspNetUsers_Email",
                table: "PublicUser",
                column: "Email",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_PublicUser_PublicUserId",
                table: "Registrations",
                column: "PublicUserId",
                principalTable: "PublicUser",
                principalColumn: "PublicUserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
