using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniTutor.Migrations
{
    /// <inheritdoc />
    public partial class subjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TutorId",
                table: "Subjects",
                newName: "tutorId");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Subjects",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Mode",
                table: "Subjects",
                newName: "mode");

            migrationBuilder.RenameColumn(
                name: "Medium",
                table: "Subjects",
                newName: "medium");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Subjects",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "CoverImage",
                table: "Subjects",
                newName: "coverImage");

            migrationBuilder.RenameColumn(
                name: "Availability",
                table: "Subjects",
                newName: "availability");

            migrationBuilder.RenameColumn(
                name: "Sub_Id",
                table: "Subjects",
                newName: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_tutorId",
                table: "Subjects",
                column: "tutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Tutors_tutorId",
                table: "Subjects",
                column: "tutorId",
                principalTable: "Tutors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Tutors_tutorId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_tutorId",
                table: "Subjects");

            migrationBuilder.RenameColumn(
                name: "tutorId",
                table: "Subjects",
                newName: "TutorId");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Subjects",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "mode",
                table: "Subjects",
                newName: "Mode");

            migrationBuilder.RenameColumn(
                name: "medium",
                table: "Subjects",
                newName: "Medium");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Subjects",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "coverImage",
                table: "Subjects",
                newName: "CoverImage");

            migrationBuilder.RenameColumn(
                name: "availability",
                table: "Subjects",
                newName: "Availability");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Subjects",
                newName: "Sub_Id");
        }
    }
}
