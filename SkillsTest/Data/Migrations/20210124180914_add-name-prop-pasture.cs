using Microsoft.EntityFrameworkCore.Migrations;

namespace SkillsTest.Data.Migrations
{
    public partial class addnameproppasture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Pasture",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Pasture");
        }
    }
}
