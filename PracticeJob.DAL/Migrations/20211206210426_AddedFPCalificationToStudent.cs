using Microsoft.EntityFrameworkCore.Migrations;

namespace PracticeJob.DAL.Migrations
{
    public partial class AddedFPCalificationToStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FPCalification",
                table: "Students",
                type: "float",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FPCalification",
                table: "Students");
        }
    }
}
