using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace discgolf_duels.Data.Migrations
{
    /// <inheritdoc />
    public partial class TestUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicUsers_AspNetUsers_Email",
                table: "PublicUsers");

            migrationBuilder.DropIndex(
                name: "IX_PublicUsers_Email",
                table: "PublicUsers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "PublicUsers");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "PublicUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PublicUsers_Id",
                table: "PublicUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicUsers_AspNetUsers_Id",
                table: "PublicUsers",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicUsers_AspNetUsers_Id",
                table: "PublicUsers");

            migrationBuilder.DropIndex(
                name: "IX_PublicUsers_Id",
                table: "PublicUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PublicUsers");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PublicUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PublicUsers_Email",
                table: "PublicUsers",
                column: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicUsers_AspNetUsers_Email",
                table: "PublicUsers",
                column: "Email",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
