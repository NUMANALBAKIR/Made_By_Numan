using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class alterbanktable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "BankAccounts",
                newName: "BankAccountId");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "BankAccounts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_AppUserId",
                table: "BankAccounts",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_AspNetUsers_AppUserId",
                table: "BankAccounts",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_AspNetUsers_AppUserId",
                table: "BankAccounts");

            migrationBuilder.DropIndex(
                name: "IX_BankAccounts_AppUserId",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "BankAccounts");

            migrationBuilder.RenameColumn(
                name: "BankAccountId",
                table: "BankAccounts",
                newName: "AccountId");
        }
    }
}
