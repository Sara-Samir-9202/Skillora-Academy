using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddDegreeToStudentCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Degree",
                table: "StudentCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Degree",
                table: "StudentCourses");
        }
    }
}
