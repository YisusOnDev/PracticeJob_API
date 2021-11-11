using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace PracticeJob.DAL.Migrations
{
    public partial class InsertProvinces : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlFile = @"../PracticeJob.DAL/Scripts/20211010_InsertProvinces.sql";
            migrationBuilder.Sql(File.ReadAllText(sqlFile));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM provinces;");
        }
    }
}
