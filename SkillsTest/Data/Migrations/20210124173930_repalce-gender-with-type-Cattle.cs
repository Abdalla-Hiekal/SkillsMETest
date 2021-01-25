using Microsoft.EntityFrameworkCore.Migrations;

namespace SkillsTest.Data.Migrations
{
    public partial class repalcegenderwithtypeCattle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Cattle");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Cattle",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Cattle");

            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "Cattle",
                nullable: false,
                defaultValue: false);
        }
    }
}
