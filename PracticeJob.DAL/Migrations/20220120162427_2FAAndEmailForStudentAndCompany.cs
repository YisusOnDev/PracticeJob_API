using Microsoft.EntityFrameworkCore.Migrations;

namespace PracticeJob.DAL.Migrations
{
    public partial class _2FAAndEmailForStudentAndCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TFCode",
                table: "Students",
                type: "longtext",
                nullable: true,
                defaultValue: null,
                collation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "ValidatedEmail",
                table: "Students",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TFCode",
                table: "Companies",
                type: "longtext",
                nullable: true,
                defaultValue: null,
                collation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "ValidatedEmail",
                table: "Companies",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TFCode",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ValidatedEmail",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TFCode",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ValidatedEmail",
                table: "Companies");
        }
    }
}
