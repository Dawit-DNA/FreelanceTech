﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FreelanceTech.Migrations
{
    public partial class modelsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_Proposal_Freelancer_freelancerId",
                table: "Proposal");

            migrationBuilder.DropIndex(
                name: "IX_Proposal_freelancerId",
                table: "Proposal");


            migrationBuilder.AlterColumn<string>(
                name: "chatItself",
                table: "Chat",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "jobId",
                table: "Chat",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "subSity",
                table: "Address",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proposal_freelancerId",
                table: "Proposal",
                column: "freelancerId");


            migrationBuilder.AddForeignKey(
                name: "FK_Proposal_Freelancer_freelancerId",
                table: "Proposal",
                column: "freelancerId",
                principalTable: "Freelancer",
                principalColumn: "freelancerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_Proposal_Freelancer_freelancerId",
                table: "Proposal");
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Freelancer_freelancerId",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Proposal_freelancerId",
                table: "Proposal");

            migrationBuilder.DropIndex(
                name: "IX_Job_freelancerId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "jobId",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "subSity",
                table: "Address");

            migrationBuilder.AddColumn<string>(
                name: "freelancerId",
                table: "Proposal",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "freelancerId",
                table: "Job",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "freelancerId",
                table: "Freelancer",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<byte[]>(
                name: "chatItself",
                table: "Chat",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proposal_freelancerId",
                table: "Proposal",
                column: "freelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_freelancerId",
                table: "Job",
                column: "freelancerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Freelancer_freelancerId",
                table: "Job",
                column: "freelancerId",
                principalTable: "Freelancer",
                principalColumn: "freelancerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Proposal_Freelancer_freelancerId",
                table: "Proposal",
                column: "freelancerId",
                principalTable: "Freelancer",
                principalColumn: "freelancerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
