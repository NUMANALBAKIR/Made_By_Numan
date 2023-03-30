using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API_Angular.Migrations
{
    /// <inheritdoc />
    public partial class apingTablesAdd2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

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

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Passed = table.Column<bool>(type: "bit", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    SubjectsListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_SubjectsLists_SubjectsListId",
                        column: x => x.SubjectsListId,
                        principalTable: "SubjectsLists",
                        principalColumn: "SubjectsListId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Students_CountryId",
                table: "Students",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SubjectsListId",
                table: "Students",
                column: "SubjectsListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "SubjectsLists");
        }
    }
}
