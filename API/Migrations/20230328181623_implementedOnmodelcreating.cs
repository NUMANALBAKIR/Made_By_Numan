using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class implementedOnmodelcreating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, "USA" },
                    { 2, "Bangladesh" }
                });

            migrationBuilder.InsertData(
                table: "SubjectsLists",
                columns: new[] { "SubjectsListId", "Accounting", "Biology", "Chemistry", "English", "Math" },
                values: new object[,]
                {
                    { 1, 77.0, 87.0, 88.0, 80.0, 65.0 },
                    { 2, 78.0, 84.0, 89.0, 60.0, 85.0 },
                    { 3, 87.0, 88.0, 85.0, 70.0, 95.0 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "CountryId", "DateOfBirth", "Gender", "Name", "Passed", "SubjectsListId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2001, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Female", "Zina", true, 1 },
                    { 2, 2, new DateTime(2002, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Male", "Sam", false, 2 },
                    { 3, 2, new DateTime(2003, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Other", "Ren", true, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 2);

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
        }
    }
}
