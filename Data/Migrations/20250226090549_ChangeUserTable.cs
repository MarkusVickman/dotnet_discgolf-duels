using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace discgolf_duels.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitions_AspNetUsers_UserEmail",
                table: "Competitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Playing_AspNetUsers_UserId",
                table: "Playing");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_AspNetUsers_UserId",
                table: "Registrations");

            migrationBuilder.DropIndex(
                name: "IX_Registrations_UserId",
                table: "Registrations");

            migrationBuilder.DropIndex(
                name: "IX_Playing_UserId",
                table: "Playing");

            migrationBuilder.DropIndex(
                name: "IX_Competitions_UserEmail",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Playing");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PDGA_Nr",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PictureURL",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "PublicUserId",
                table: "Registrations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PublicUserId",
                table: "Playing",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PublicUserId",
                table: "Competitions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PublicUser",
                columns: table => new
                {
                    PublicUserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DisplayName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PDGA_Nr = table.Column<int>(type: "INTEGER", nullable: true),
                    PictureURL = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicUser", x => x.PublicUserId);
                    table.ForeignKey(
                        name: "FK_PublicUser_AspNetUsers_Email",
                        column: x => x.Email,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_PublicUserId",
                table: "Registrations",
                column: "PublicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Playing_PublicUserId",
                table: "Playing",
                column: "PublicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_PublicUserId",
                table: "Competitions",
                column: "PublicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicUser_Email",
                table: "PublicUser",
                column: "Email");

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
                name: "FK_Registrations_PublicUser_PublicUserId",
                table: "Registrations",
                column: "PublicUserId",
                principalTable: "PublicUser",
                principalColumn: "PublicUserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitions_PublicUser_PublicUserId",
                table: "Competitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Playing_PublicUser_PublicUserId",
                table: "Playing");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_PublicUser_PublicUserId",
                table: "Registrations");

            migrationBuilder.DropTable(
                name: "PublicUser");

            migrationBuilder.DropIndex(
                name: "IX_Registrations_PublicUserId",
                table: "Registrations");

            migrationBuilder.DropIndex(
                name: "IX_Playing_PublicUserId",
                table: "Playing");

            migrationBuilder.DropIndex(
                name: "IX_Competitions_PublicUserId",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "PublicUserId",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "PublicUserId",
                table: "Playing");

            migrationBuilder.DropColumn(
                name: "PublicUserId",
                table: "Competitions");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Registrations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Playing",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Competitions",
                type: "TEXT",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PDGA_Nr",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureURL",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_UserId",
                table: "Registrations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Playing_UserId",
                table: "Playing",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_UserEmail",
                table: "Competitions",
                column: "UserEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitions_AspNetUsers_UserEmail",
                table: "Competitions",
                column: "UserEmail",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Playing_AspNetUsers_UserId",
                table: "Playing",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_AspNetUsers_UserId",
                table: "Registrations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
