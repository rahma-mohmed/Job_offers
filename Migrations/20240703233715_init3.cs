using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Job_offers.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CVFileName",
                table: "ApplyForJobs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVFileName",
                table: "ApplyForJobs");
        }
    }
}
