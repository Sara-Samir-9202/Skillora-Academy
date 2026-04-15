using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentSystem.Migrations
{
    /// <inheritdoc />
    public partial class FixInstructorRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Departments_DepartmentDeptId",
                table: "Instructors");

            migrationBuilder.DropIndex(
                name: "IX_Instructors_DepartmentDeptId",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "DepartmentDeptId",
                table: "Instructors");

            migrationBuilder.AlterColumn<int>(
                name: "MinDegree",
                table: "Courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MaxDegree",
                table: "Courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_DeptId",
                table: "Instructors",
                column: "DeptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Departments_DeptId",
                table: "Instructors",
                column: "DeptId",
                principalTable: "Departments",
                principalColumn: "DeptId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Departments_DeptId",
                table: "Instructors");

            migrationBuilder.DropIndex(
                name: "IX_Instructors_DeptId",
                table: "Instructors");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentDeptId",
                table: "Instructors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MinDegree",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaxDegree",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_DepartmentDeptId",
                table: "Instructors",
                column: "DepartmentDeptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Departments_DepartmentDeptId",
                table: "Instructors",
                column: "DepartmentDeptId",
                principalTable: "Departments",
                principalColumn: "DeptId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
