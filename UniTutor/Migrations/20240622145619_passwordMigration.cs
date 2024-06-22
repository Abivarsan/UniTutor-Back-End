using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniTutor.Migrations
{
    /// <inheritdoc />
    public partial class passwordMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Tutors",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Students",
                newName: "password");

            migrationBuilder.AlterColumn<int>(
                name: "HomeTown",
                table: "Students",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "Tutors",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Students",
                newName: "Password");

            migrationBuilder.AlterColumn<string>(
                name: "HomeTown",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
