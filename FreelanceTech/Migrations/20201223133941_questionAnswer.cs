using Microsoft.EntityFrameworkCore.Migrations;

namespace FreelanceTech.Migrations
{
    public partial class questionAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "answers",
                table: "Proposal",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "questions",
                table: "Job",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "answers",
                table: "Proposal");

            migrationBuilder.DropColumn(
                name: "questions",
                table: "Job");
        }
    }
}
