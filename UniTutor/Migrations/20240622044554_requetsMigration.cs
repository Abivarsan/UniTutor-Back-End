using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniTutor.Migrations
{
    /// <inheritdoc />
    public partial class requetsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ModeOfTeaching",
                table: "Tutors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "Medium",
                table: "Tutors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Qualification",
                table: "Tutors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "subject",
                table: "Tutors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medium = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Availability = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    Student_Id = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Tutor_id = table.Column<int>(type: "int", nullable: false),
                    TutorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_Request_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_Tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Request_StudentId",
                table: "Request",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_TutorId",
                table: "Request",
                column: "TutorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropColumn(
                name: "Medium",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "Qualification",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "subject",
                table: "Tutors");

            migrationBuilder.AlterColumn<bool>(
                name: "ModeOfTeaching",
                table: "Tutors",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
