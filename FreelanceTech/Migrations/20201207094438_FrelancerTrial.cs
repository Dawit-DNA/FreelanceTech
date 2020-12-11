﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FreelanceTech.Migrations
{
    public partial class FrelancerTrial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    userId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    region = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true),
                    woreda = table.Column<string>(nullable: true),
                    houseNumber = table.Column<int>(nullable: false),
                    pobox = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    chatId = table.Column<string>(nullable: false),
                    firstUser = table.Column<string>(nullable: true),
                    secondUser = table.Column<string>(nullable: true),
                    chatItself = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.chatId);
                });

            migrationBuilder.CreateTable(
                name: "Expertise",
                columns: table => new
                {
                    categoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expertise", x => x.categoryId);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    language = table.Column<string>(nullable: false),
                    userId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.language);
                });

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    userId = table.Column<string>(nullable: false),
                    balance = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    customerId = table.Column<string>(nullable: false),
                    photo = table.Column<byte[]>(nullable: true),
                    legalId = table.Column<byte[]>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    language = table.Column<int>(nullable: false),
                    phoneNumber = table.Column<int>(nullable: false),
                    addressuserId = table.Column<int>(nullable: true),
                    language1 = table.Column<string>(nullable: true),
                    WalletuserId = table.Column<string>(nullable: true),
                    englishProficiency = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.customerId);
                    table.ForeignKey(
                        name: "FK_Customer_Wallet_WalletuserId",
                        column: x => x.WalletuserId,
                        principalTable: "Wallet",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_Address_addressuserId",
                        column: x => x.addressuserId,
                        principalTable: "Address",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_Language_language1",
                        column: x => x.language1,
                        principalTable: "Language",
                        principalColumn: "language",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Freelancer",
                columns: table => new
                {
                    freelancerId = table.Column<string>(nullable: false),
                    phoneNumber = table.Column<string>(nullable: true),
                    ExpertisecategoryId = table.Column<int>(nullable: true),
                    education = table.Column<string>(nullable: true),
                    id = table.Column<int>(nullable: false),
                    language = table.Column<string>(nullable: true),
                    englishProficiency = table.Column<string>(nullable: true),
                    hourlyRate = table.Column<string>(nullable: true),
                    title = table.Column<string>(nullable: true),
                    professionalOverview = table.Column<string>(nullable: true),
                    photo = table.Column<byte[]>(nullable: true),
                    AddressuserId = table.Column<int>(nullable: true),
                    WalletuserId = table.Column<string>(nullable: true),
                    legaID = table.Column<byte[]>(nullable: true),
                    rate = table.Column<double>(nullable: false),
                    score = table.Column<double>(nullable: false),
                    status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Freelancer", x => x.freelancerId);
                    table.ForeignKey(
                        name: "FK_Freelancer_Address_AddressuserId",
                        column: x => x.AddressuserId,
                        principalTable: "Address",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Freelancer_Expertise_ExpertisecategoryId",
                        column: x => x.ExpertisecategoryId,
                        principalTable: "Expertise",
                        principalColumn: "categoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Freelancer_Wallet_WalletuserId",
                        column: x => x.WalletuserId,
                        principalTable: "Wallet",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Freelancer_Language_language",
                        column: x => x.language,
                        principalTable: "Language",
                        principalColumn: "language",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Experience",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    freelancerId = table.Column<string>(nullable: true),
                    companyName = table.Column<string>(nullable: true),
                    location = table.Column<string>(nullable: true),
                    title = table.Column<string>(nullable: true),
                    startDate = table.Column<DateTime>(nullable: false),
                    endDate = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experience", x => x.id);
                    table.ForeignKey(
                        name: "FK_Experience_Freelancer_freelancerId",
                        column: x => x.freelancerId,
                        principalTable: "Freelancer",
                        principalColumn: "freelancerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    jobId = table.Column<string>(nullable: false),
                    status = table.Column<string>(nullable: true),
                    title = table.Column<string>(nullable: true),
                    category = table.Column<string>(nullable: true),
                    startDate = table.Column<DateTime>(nullable: false),
                    endDate = table.Column<DateTime>(nullable: false),
                    startPrice = table.Column<double>(nullable: false),
                    endPrice = table.Column<double>(nullable: false),
                    level = table.Column<string>(nullable: true),
                    Payment_Amount = table.Column<double>(nullable: false),
                    customerId = table.Column<string>(nullable: true),
                    freelancerId = table.Column<int>(nullable: false),
                    freelancerId1 = table.Column<string>(nullable: true),
                    contractId = table.Column<int>(nullable: false),
                    businessAnalystId = table.Column<string>(nullable: true),
                    comment = table.Column<string>(nullable: true),
                    rate = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.jobId);
                    table.ForeignKey(
                        name: "FK_Job_Customer_customerId",
                        column: x => x.customerId,
                        principalTable: "Customer",
                        principalColumn: "customerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Job_Freelancer_freelancerId1",
                        column: x => x.freelancerId1,
                        principalTable: "Freelancer",
                        principalColumn: "freelancerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    transactionId = table.Column<string>(nullable: false),
                    userId = table.Column<string>(nullable: true),
                    amount = table.Column<double>(nullable: false),
                    time = table.Column<DateTime>(nullable: false),
                    customerId = table.Column<string>(nullable: true),
                    freelancerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.transactionId);
                    table.ForeignKey(
                        name: "FK_Transaction_Customer_customerId",
                        column: x => x.customerId,
                        principalTable: "Customer",
                        principalColumn: "customerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Freelancer_freelancerId",
                        column: x => x.freelancerId,
                        principalTable: "Freelancer",
                        principalColumn: "freelancerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    contractId = table.Column<string>(nullable: false),
                    file = table.Column<int>(nullable: false),
                    jobId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.contractId);
                    table.ForeignKey(
                        name: "FK_Contract_Job_jobId",
                        column: x => x.jobId,
                        principalTable: "Job",
                        principalColumn: "jobId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Proposal",
                columns: table => new
                {
                    proposalId = table.Column<string>(nullable: false),
                    jobId = table.Column<string>(nullable: true),
                    freelancerId = table.Column<int>(nullable: false),
                    freelancerId1 = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    bidAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposal", x => x.proposalId);
                    table.ForeignKey(
                        name: "FK_Proposal_Freelancer_freelancerId1",
                        column: x => x.freelancerId1,
                        principalTable: "Freelancer",
                        principalColumn: "freelancerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proposal_Job_jobId",
                        column: x => x.jobId,
                        principalTable: "Job",
                        principalColumn: "jobId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_jobId",
                table: "Contract",
                column: "jobId",
                unique: true,
                filter: "[jobId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_WalletuserId",
                table: "Customer",
                column: "WalletuserId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_addressuserId",
                table: "Customer",
                column: "addressuserId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_language1",
                table: "Customer",
                column: "language1");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_freelancerId",
                table: "Experience",
                column: "freelancerId",
                unique: true,
                filter: "[freelancerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Freelancer_AddressuserId",
                table: "Freelancer",
                column: "AddressuserId");

            migrationBuilder.CreateIndex(
                name: "IX_Freelancer_ExpertisecategoryId",
                table: "Freelancer",
                column: "ExpertisecategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Freelancer_WalletuserId",
                table: "Freelancer",
                column: "WalletuserId");

            migrationBuilder.CreateIndex(
                name: "IX_Freelancer_language",
                table: "Freelancer",
                column: "language");

            migrationBuilder.CreateIndex(
                name: "IX_Job_customerId",
                table: "Job",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_freelancerId1",
                table: "Job",
                column: "freelancerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Proposal_freelancerId1",
                table: "Proposal",
                column: "freelancerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Proposal_jobId",
                table: "Proposal",
                column: "jobId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_customerId",
                table: "Transaction",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_freelancerId",
                table: "Transaction",
                column: "freelancerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Experience");

            migrationBuilder.DropTable(
                name: "Proposal");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Freelancer");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Expertise");

            migrationBuilder.DropTable(
                name: "Wallet");

            migrationBuilder.DropTable(
                name: "Language");
        }
    }
}
