using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Angular.Migrations
{
    /// <inheritdoc />
    public partial class changedSubject2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Students_StudentId",
                table: "Subjects");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Students_StudentId",
                table: "Subjects",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Students_StudentId",
                table: "Subjects");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Subjects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Students_StudentId",
                table: "Subjects",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");
        }
    }
}
