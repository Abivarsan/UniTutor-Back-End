using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniTutor.Migrations
{
    /// <inheritdoc />
    public partial class tutor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Tutors_TutorId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_TutorId",
                table: "Subjects");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Subjects",
                newName: "Sub_Id");

            migrationBuilder.AlterColumn<string>(
                name: "Ocupation",
                table: "Tutors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Qualification",
                table: "Tutors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qualification",
                table: "Tutors");

            migrationBuilder.RenameColumn(
                name: "Sub_Id",
                table: "Subjects",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Ocupation",
                table: "Tutors",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_TutorId",
                table: "Subjects",
                column: "TutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Tutors_TutorId",
                table: "Subjects",
                column: "TutorId",
                principalTable: "Tutors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
