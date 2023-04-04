using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API_Angular.Migrations
{
    /// <inheritdoc />
    public partial class addedSubjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SubjectsLists",
                keyColumn: "SubjectsListId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SubjectsLists",
                keyColumn: "SubjectsListId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SubjectsLists",
                keyColumn: "SubjectsListId",
                keyValue: 3);

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                    table.ForeignKey(
                        name: "FK_Subjects_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_StudentId",
                table: "Subjects",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.InsertData(
                table: "SubjectsLists",
                columns: new[] { "SubjectsListId", "Accounting", "Biology", "Chemistry", "English", "Math" },
                values: new object[,]
                {
                    { 1, 77.0, 87.0, 88.0, 80.0, 65.0 },
                    { 2, 78.0, 84.0, 89.0, 60.0, 85.0 },
                    { 3, 87.0, 88.0, 85.0, 70.0, 95.0 }
                });
        }
    }
}
