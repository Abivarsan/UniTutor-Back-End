using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniTutor.Migrations
{
    /// <inheritdoc />
    public partial class correction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Tutors",
                newName: "phoneNumber");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Tutors",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Tutors",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Tutors",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "UniIdUrl",
                table: "Tutors",
                newName: "universityID");

            migrationBuilder.RenameColumn(
                name: "Qualification",
                table: "Tutors",
                newName: "qualifications");

            migrationBuilder.RenameColumn(
                name: "Ocupation",
                table: "Tutors",
                newName: "occupation");

            migrationBuilder.RenameColumn(
                name: "HomeTown",
                table: "Tutors",
                newName: "district");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Tutors",
                newName: "universityMail");

            migrationBuilder.RenameColumn(
                name: "CVUrl",
                table: "Tutors",
                newName: "cv");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "phoneNumber",
                table: "Tutors",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Tutors",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Tutors",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Tutors",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "universityMail",
                table: "Tutors",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "universityID",
                table: "Tutors",
                newName: "UniIdUrl");

            migrationBuilder.RenameColumn(
                name: "qualifications",
                table: "Tutors",
                newName: "Qualification");

            migrationBuilder.RenameColumn(
                name: "occupation",
                table: "Tutors",
                newName: "Ocupation");

            migrationBuilder.RenameColumn(
                name: "district",
                table: "Tutors",
                newName: "HomeTown");

            migrationBuilder.RenameColumn(
                name: "cv",
                table: "Tutors",
                newName: "CVUrl");
        }
    }
}
