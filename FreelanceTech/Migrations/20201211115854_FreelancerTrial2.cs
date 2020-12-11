using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FreelanceTech.Migrations
{
    public partial class FreelancerTrial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Language_language1",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Freelancer_Expertise_ExpertisecategoryId",
                table: "Freelancer");

            migrationBuilder.DropForeignKey(
                name: "FK_Freelancer_Language_language",
                table: "Freelancer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Language",
                table: "Language");

            migrationBuilder.DropIndex(
                name: "IX_Freelancer_ExpertisecategoryId",
                table: "Freelancer");

            migrationBuilder.DropIndex(
                name: "IX_Freelancer_language",
                table: "Freelancer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Expertise",
                table: "Expertise");

            migrationBuilder.DropIndex(
                name: "IX_Customer_language1",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ExpertisecategoryId",
                table: "Freelancer");

            migrationBuilder.DropColumn(
                name: "language",
                table: "Freelancer");

            migrationBuilder.DropColumn(
                name: "categoryId",
                table: "Expertise");

            migrationBuilder.DropColumn(
                name: "language1",
                table: "Customer");

            migrationBuilder.AlterColumn<string>(
                name: "language",
                table: "Language",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "Language",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "freelancerId",
                table: "Language",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "postedDate",
                table: "Job",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "Expertise",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "freelancerId",
                table: "Expertise",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "skill",
                table: "Expertise",
                nullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "photo",
                table: "Customer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Languageid",
                table: "Customer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "file",
                table: "Contract",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "signedDate",
                table: "Contract",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Language",
                table: "Language",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Expertise",
                table: "Expertise",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Language_freelancerId",
                table: "Language",
                column: "freelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_Expertise_freelancerId",
                table: "Expertise",
                column: "freelancerId",
                unique: true,
                filter: "[freelancerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Languageid",
                table: "Customer",
                column: "Languageid");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Language_Languageid",
                table: "Customer",
                column: "Languageid",
                principalTable: "Language",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expertise_Freelancer_freelancerId",
                table: "Expertise",
                column: "freelancerId",
                principalTable: "Freelancer",
                principalColumn: "freelancerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Language_Freelancer_freelancerId",
                table: "Language",
                column: "freelancerId",
                principalTable: "Freelancer",
                principalColumn: "freelancerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Language_Languageid",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Expertise_Freelancer_freelancerId",
                table: "Expertise");

            migrationBuilder.DropForeignKey(
                name: "FK_Language_Freelancer_freelancerId",
                table: "Language");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Language",
                table: "Language");

            migrationBuilder.DropIndex(
                name: "IX_Language_freelancerId",
                table: "Language");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Expertise",
                table: "Expertise");

            migrationBuilder.DropIndex(
                name: "IX_Expertise_freelancerId",
                table: "Expertise");

            migrationBuilder.DropIndex(
                name: "IX_Customer_Languageid",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Language");

            migrationBuilder.DropColumn(
                name: "freelancerId",
                table: "Language");

            migrationBuilder.DropColumn(
                name: "postedDate",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Expertise");

            migrationBuilder.DropColumn(
                name: "freelancerId",
                table: "Expertise");

            migrationBuilder.DropColumn(
                name: "skill",
                table: "Expertise");

            migrationBuilder.DropColumn(
                name: "Languageid",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "signedDate",
                table: "Contract");

            migrationBuilder.AlterColumn<string>(
                name: "language",
                table: "Language",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExpertisecategoryId",
                table: "Freelancer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "language",
                table: "Freelancer",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "categoryId",
                table: "Expertise",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<byte[]>(
                name: "photo",
                table: "Customer",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "language1",
                table: "Customer",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "file",
                table: "Contract",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Language",
                table: "Language",
                column: "language");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Expertise",
                table: "Expertise",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Freelancer_ExpertisecategoryId",
                table: "Freelancer",
                column: "ExpertisecategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Freelancer_language",
                table: "Freelancer",
                column: "language");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_language1",
                table: "Customer",
                column: "language1");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Language_language1",
                table: "Customer",
                column: "language1",
                principalTable: "Language",
                principalColumn: "language",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Freelancer_Expertise_ExpertisecategoryId",
                table: "Freelancer",
                column: "ExpertisecategoryId",
                principalTable: "Expertise",
                principalColumn: "categoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Freelancer_Language_language",
                table: "Freelancer",
                column: "language",
                principalTable: "Language",
                principalColumn: "language",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
