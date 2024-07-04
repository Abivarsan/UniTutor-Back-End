using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniTutor.Migrations
{
    /// <inheritdoc />
    public partial class nilaxan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomeTown",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Students",
                newName: "phoneNumber");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Students",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "Grade",
                table: "Students",
                newName: "grade");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Students",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Students",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Students",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "School",
                table: "Students",
                newName: "schoolName");

            migrationBuilder.AlterColumn<string>(
                name: "grade",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "district",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "district",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "phoneNumber",
                table: "Students",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Students",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "grade",
                table: "Students",
                newName: "Grade");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Students",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Students",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Students",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "schoolName",
                table: "Students",
                newName: "School");

            migrationBuilder.AlterColumn<int>(
                name: "Grade",
                table: "Students",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "HomeTown",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
