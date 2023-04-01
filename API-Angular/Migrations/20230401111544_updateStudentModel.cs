using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Angular.Migrations
{
    /// <inheritdoc />
    public partial class updateStudentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_SubjectsLists_SubjectsListId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SubjectsListId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SubjectsListId",
                table: "Students");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectsListId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1,
                column: "SubjectsListId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 2,
                column: "SubjectsListId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 3,
                column: "SubjectsListId",
                value: 3);

            migrationBuilder.CreateIndex(
                name: "IX_Students_SubjectsListId",
                table: "Students",
                column: "SubjectsListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_SubjectsLists_SubjectsListId",
                table: "Students",
                column: "SubjectsListId",
                principalTable: "SubjectsLists",
                principalColumn: "SubjectsListId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
