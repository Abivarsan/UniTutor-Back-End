using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniTutor.Migrations
{
    /// <inheritdoc />
    public partial class final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVContentType",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "CVData",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "CVFileName",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "UniIDContentType",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "UniIDData",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "UniIDFileName",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Students",
                newName: "ProfileImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "CVUrl",
                table: "Tutors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UniIdUrl",
                table: "Tutors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "School",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVUrl",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "UniIdUrl",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "School",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "ProfileImageUrl",
                table: "Students",
                newName: "FileName");

            migrationBuilder.AddColumn<string>(
                name: "CVContentType",
                table: "Tutors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "CVData",
                table: "Tutors",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CVFileName",
                table: "Tutors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniIDContentType",
                table: "Tutors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "UniIDData",
                table: "Tutors",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniIDFileName",
                table: "Tutors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Students",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
