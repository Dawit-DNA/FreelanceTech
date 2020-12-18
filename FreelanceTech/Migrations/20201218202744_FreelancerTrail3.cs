using Microsoft.EntityFrameworkCore.Migrations;

namespace FreelanceTech.Migrations
{
    public partial class FreelancerTrail3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "level",
                table: "Expertise",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Expertise",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JobSkill",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    jobId = table.Column<string>(nullable: true),
                    skill = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSkill", x => x.id);
                    table.ForeignKey(
                        name: "FK_JobSkill_Job_jobId",
                        column: x => x.jobId,
                        principalTable: "Job",
                        principalColumn: "jobId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobSkill_jobId",
                table: "JobSkill",
                column: "jobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobSkill");

            migrationBuilder.DropColumn(
                name: "level",
                table: "Expertise");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Expertise");
        }
    }
}
