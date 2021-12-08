using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace PracticeJob.DAL.Migrations
{
    public partial class AddFPData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlFile = @"../PracticeJob.DAL/Scripts/20211206_InsertFPData.sql";
            migrationBuilder.Sql(File.ReadAllText(sqlFile));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM fpgrades;");
            migrationBuilder.Sql("DELETE FROM fpfamilies;");
            migrationBuilder.Sql("DELETE FROM fps;");
        }
    }
}
