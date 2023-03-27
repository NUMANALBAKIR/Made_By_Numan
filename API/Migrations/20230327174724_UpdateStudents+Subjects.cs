using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStudentsSubjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.RenameColumn(
                name: "Pass",
                table: "Students",
                newName: "PassedOrFailed");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubjectsListId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SubjectsLists",
                columns: table => new
                {
                    SubjectsListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    English = table.Column<double>(type: "float", nullable: false),
                    Math = table.Column<double>(type: "float", nullable: false),
                    Accounting = table.Column<double>(type: "float", nullable: false),
                    Chemistry = table.Column<double>(type: "float", nullable: false),
                    Biology = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectsLists", x => x.SubjectsListId);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_SubjectsLists_SubjectsListId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "SubjectsLists");

            migrationBuilder.DropIndex(
                name: "IX_Students_SubjectsListId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SubjectsListId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "PassedOrFailed",
                table: "Students",
                newName: "Pass");

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                });
        }
    }
}
