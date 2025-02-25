using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace discgolf_duels.Data.Migrations
{
    /// <inheritdoc />
    public partial class NormalizedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plays_AspNetUsers_UserEmail",
                table: "Plays");

            migrationBuilder.DropIndex(
                name: "IX_Plays_UserEmail",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "GroupNr",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "Par",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "RegisterDate",
                table: "Competitions");

            migrationBuilder.CreateTable(
                name: "Playing",
                columns: table => new
                {
                    PlayingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayId = table.Column<int>(type: "INTEGER", nullable: false),
                    Par = table.Column<int>(type: "INTEGER", nullable: true),
                    GroupNr = table.Column<int>(type: "INTEGER", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playing", x => x.PlayingId);
                    table.ForeignKey(
                        name: "FK_Playing_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Playing_Plays_PlayId",
                        column: x => x.PlayId,
                        principalTable: "Plays",
                        principalColumn: "PlayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    RegistrationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompetitionId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.RegistrationId);
                    table.ForeignKey(
                        name: "FK_Registrations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registrations_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "CompetitionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Playing_PlayId",
                table: "Playing",
                column: "PlayId");

            migrationBuilder.CreateIndex(
                name: "IX_Playing_UserId",
                table: "Playing",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_CompetitionId",
                table: "Registrations",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_UserId",
                table: "Registrations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Playing");

            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.AddColumn<int>(
                name: "GroupNr",
                table: "Plays",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Par",
                table: "Plays",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "Plays",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Plays",
                type: "TEXT",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterDate",
                table: "Competitions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plays_UserEmail",
                table: "Plays",
                column: "UserEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Plays_AspNetUsers_UserEmail",
                table: "Plays",
                column: "UserEmail",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
