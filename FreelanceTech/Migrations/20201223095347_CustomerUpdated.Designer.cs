﻿// <auto-generated />
using System;
using FreelanceTech.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FreelanceTech.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201223095347_CustomerUpdated")]
    partial class CustomerUpdated
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FreelanceTech.Models.Address", b =>
                {
                    b.Property<string>("userId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("city")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("houseNumber")
                        .HasColumnType("int");

                    b.Property<int>("pobox")
                        .HasColumnType("int");

                    b.Property<string>("region")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("woreda")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("FreelanceTech.Models.Chat", b =>
                {
                    b.Property<string>("chatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("chatItself")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("firstUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("secondUser")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("chatId");

                    b.ToTable("Chat");
                });

            modelBuilder.Entity("FreelanceTech.Models.Contract", b =>
                {
                    b.Property<string>("contractId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("file")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("jobId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("signedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("contractId");

                    b.HasIndex("jobId")
                        .IsUnique()
                        .HasFilter("[jobId] IS NOT NULL");

                    b.ToTable("Contract");
                });

            modelBuilder.Entity("FreelanceTech.Models.Customer", b =>
                {
                    b.Property<string>("customerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Languageid")
                        .HasColumnType("int");

                    b.Property<string>("WalletuserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("addressuserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("englishProficiency")
                        .HasColumnType("int");

                    b.Property<int>("language")
                        .HasColumnType("int");

                    b.Property<string>("legalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("customerId");

                    b.HasIndex("Languageid");

                    b.HasIndex("WalletuserId");

                    b.HasIndex("addressuserId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("FreelanceTech.Models.Experience", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("companyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("endDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("freelancerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("startDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("freelancerId")
                        .IsUnique()
                        .HasFilter("[freelancerId] IS NOT NULL");

                    b.ToTable("Experience");
                });

            modelBuilder.Entity("FreelanceTech.Models.Expertise", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("category")
                        .HasColumnType("int");

                    b.Property<string>("freelancerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("level")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("skill")
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("freelancerId")
                        .IsUnique()
                        .HasFilter("[freelancerId] IS NOT NULL");

                    b.ToTable("Expertise");
                });

            modelBuilder.Entity("FreelanceTech.Models.Freelancer", b =>
                {
                    b.Property<string>("freelancerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddressuserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("WalletuserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("education")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("englishProficiency")
                        .HasColumnType("int");

                    b.Property<double>("hourlyRate")
                        .HasColumnType("float");

                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<string>("legaID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("professionalOverview")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("rate")
                        .HasColumnType("float");

                    b.Property<double>("score")
                        .HasColumnType("float");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("freelancerId");

                    b.HasIndex("AddressuserId");

                    b.HasIndex("WalletuserId");

                    b.ToTable("Freelancer");
                });

            modelBuilder.Entity("FreelanceTech.Models.Job", b =>
                {
                    b.Property<string>("jobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Payment_Amount")
                        .HasColumnType("float");

                    b.Property<string>("businessAnalystId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("contractId")
                        .HasColumnType("int");

                    b.Property<string>("customerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("endDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("endPrice")
                        .HasColumnType("float");

                    b.Property<int>("freelancerId")
                        .HasColumnType("int");

                    b.Property<string>("freelancerId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("level")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("postedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("rate")
                        .HasColumnType("int");

                    b.Property<DateTime>("startDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("startPrice")
                        .HasColumnType("float");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("jobId");

                    b.HasIndex("customerId");

                    b.HasIndex("freelancerId1");

                    b.ToTable("Job");
                });

            modelBuilder.Entity("FreelanceTech.Models.JobSkill", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("jobId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("skill")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("jobId");

                    b.ToTable("JobSkill");
                });

            modelBuilder.Entity("FreelanceTech.Models.Language", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("freelancerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("language")
                        .HasColumnType("int");

                    b.Property<string>("userId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("freelancerId");

                    b.ToTable("Language");
                });

            modelBuilder.Entity("FreelanceTech.Models.Proposal", b =>
                {
                    b.Property<string>("proposalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("bidAmount")
                        .HasColumnType("float");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("freelancerId")
                        .HasColumnType("int");

                    b.Property<string>("freelancerId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("jobId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("proposalId");

                    b.HasIndex("freelancerId1");

                    b.HasIndex("jobId");

                    b.ToTable("Proposal");
                });

            modelBuilder.Entity("FreelanceTech.Models.Transaction", b =>
                {
                    b.Property<string>("transactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("amount")
                        .HasColumnType("float");

                    b.Property<string>("customerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("freelancerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("time")
                        .HasColumnType("datetime2");

                    b.Property<string>("userId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("transactionId");

                    b.HasIndex("customerId");

                    b.HasIndex("freelancerId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("FreelanceTech.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("firstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("FreelanceTech.Models.Wallet", b =>
                {
                    b.Property<string>("userId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("balance")
                        .HasColumnType("float");

                    b.HasKey("userId");

                    b.ToTable("Wallet");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("FreelanceTech.Models.Contract", b =>
                {
                    b.HasOne("FreelanceTech.Models.Job", "Job")
                        .WithOne("Contract")
                        .HasForeignKey("FreelanceTech.Models.Contract", "jobId");
                });

            modelBuilder.Entity("FreelanceTech.Models.Customer", b =>
                {
                    b.HasOne("FreelanceTech.Models.Language", "Language")
                        .WithMany()
                        .HasForeignKey("Languageid");

                    b.HasOne("FreelanceTech.Models.Wallet", "Wallet")
                        .WithMany()
                        .HasForeignKey("WalletuserId");

                    b.HasOne("FreelanceTech.Models.Address", "address")
                        .WithMany()
                        .HasForeignKey("addressuserId");
                });

            modelBuilder.Entity("FreelanceTech.Models.Experience", b =>
                {
                    b.HasOne("FreelanceTech.Models.Freelancer", "Freelancer")
                        .WithOne("Experience")
                        .HasForeignKey("FreelanceTech.Models.Experience", "freelancerId");
                });

            modelBuilder.Entity("FreelanceTech.Models.Expertise", b =>
                {
                    b.HasOne("FreelanceTech.Models.Freelancer", null)
                        .WithOne("Expertise")
                        .HasForeignKey("FreelanceTech.Models.Expertise", "freelancerId");
                });

            modelBuilder.Entity("FreelanceTech.Models.Freelancer", b =>
                {
                    b.HasOne("FreelanceTech.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressuserId");

                    b.HasOne("FreelanceTech.Models.Wallet", "Wallet")
                        .WithMany()
                        .HasForeignKey("WalletuserId");
                });

            modelBuilder.Entity("FreelanceTech.Models.Job", b =>
                {
                    b.HasOne("FreelanceTech.Models.Customer", "Customer")
                        .WithMany("Job")
                        .HasForeignKey("customerId");

                    b.HasOne("FreelanceTech.Models.Freelancer", "Freelancer")
                        .WithMany("Job")
                        .HasForeignKey("freelancerId1");
                });

            modelBuilder.Entity("FreelanceTech.Models.JobSkill", b =>
                {
                    b.HasOne("FreelanceTech.Models.Job", "Job")
                        .WithMany("JobSkill")
                        .HasForeignKey("jobId");
                });

            modelBuilder.Entity("FreelanceTech.Models.Language", b =>
                {
                    b.HasOne("FreelanceTech.Models.Freelancer", null)
                        .WithMany("Language")
                        .HasForeignKey("freelancerId");
                });

            modelBuilder.Entity("FreelanceTech.Models.Proposal", b =>
                {
                    b.HasOne("FreelanceTech.Models.Freelancer", "Freelancer")
                        .WithMany("Proposal")
                        .HasForeignKey("freelancerId1");

                    b.HasOne("FreelanceTech.Models.Job", "Job")
                        .WithMany("Proposal")
                        .HasForeignKey("jobId");
                });

            modelBuilder.Entity("FreelanceTech.Models.Transaction", b =>
                {
                    b.HasOne("FreelanceTech.Models.Customer", null)
                        .WithMany("Transaction")
                        .HasForeignKey("customerId");

                    b.HasOne("FreelanceTech.Models.Freelancer", null)
                        .WithMany("Transaction")
                        .HasForeignKey("freelancerId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("FreelanceTech.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FreelanceTech.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FreelanceTech.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("FreelanceTech.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
