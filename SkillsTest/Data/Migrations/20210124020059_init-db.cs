using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkillsTest.Data.Migrations
{
    public partial class initdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pasture",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Temperature = table.Column<int>(nullable: false),
                    GrassCondition = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pasture", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cattle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Age = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    Weight = table.Column<double>(nullable: false),
                    Gender = table.Column<bool>(nullable: false),
                    HealthStatus = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    PastureId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cattle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cattle_Pasture_PastureId",
                        column: x => x.PastureId,
                        principalTable: "Pasture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cattle_PastureId",
                table: "Cattle",
                column: "PastureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cattle");

            migrationBuilder.DropTable(
                name: "Pasture");
        }
    }
}
