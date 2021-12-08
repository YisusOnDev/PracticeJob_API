using Microsoft.EntityFrameworkCore.Migrations;

namespace PracticeJob.DAL.Migrations
{
    public partial class AddedFPToStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FPId",
                table: "Students",
                type: "int",
                nullable: true,
                defaultValue: null);

            migrationBuilder.CreateIndex(
                name: "IX_Students_FPId",
                table: "Students",
                column: "FPId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_FPs_FPId",
                table: "Students",
                column: "FPId",
                principalTable: "FPs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_FPs_FPId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_FPId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FPId",
                table: "Students");
        }
    }
}
