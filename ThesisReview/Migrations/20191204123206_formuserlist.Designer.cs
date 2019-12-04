﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThesisReview.Data;

namespace ThesisReview.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20191204123206_formuserlist")]
    partial class formuserlist
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
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
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ThesisReview.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Department");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Fullname");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Title");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ThesisReview.Data.Models.Form", b =>
                {
                    b.Property<int>("FormId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTimeFinish");

                    b.Property<DateTime>("DateTimeStart");

                    b.Property<string>("Department")
                        .IsRequired();

                    b.Property<string>("FormURL");

                    b.Property<string>("GuardianName")
                        .IsRequired();

                    b.Property<string>("GuardianUserUserListId");

                    b.Property<string>("Link");

                    b.Property<string>("Password");

                    b.Property<int?>("QuestionsGuardianQuestionsId");

                    b.Property<int?>("QuestionsId");

                    b.Property<string>("ReviewType")
                        .IsRequired();

                    b.Property<string>("ReviewerName");

                    b.Property<string>("ReviewerUserUserListId");

                    b.Property<string>("ShortDescription")
                        .IsRequired();

                    b.Property<string>("Status");

                    b.Property<string>("StudentMail")
                        .IsRequired();

                    b.Property<string>("StudentName")
                        .IsRequired();

                    b.Property<byte[]>("ThesisFile");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("FormId");

                    b.HasIndex("GuardianUserUserListId");

                    b.HasIndex("QuestionsGuardianQuestionsId");

                    b.HasIndex("QuestionsId");

                    b.HasIndex("ReviewerUserUserListId");

                    b.ToTable("Forms");
                });

            modelBuilder.Entity("ThesisReview.Data.Models.Questions", b =>
                {
                    b.Property<int>("QuestionsId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Finished");

                    b.Property<string>("FormURL");

                    b.Property<string>("Grade");

                    b.Property<string>("LongReview");

                    b.Property<string>("Mail");

                    b.Property<int>("Points");

                    b.Property<string>("Question0");

                    b.Property<string>("Question1");

                    b.Property<string>("Question2");

                    b.Property<string>("Question3");

                    b.Property<string>("Question4");

                    b.Property<string>("Question5");

                    b.Property<string>("Question6");

                    b.Property<string>("Question7");

                    b.Property<string>("Question8");

                    b.Property<string>("Question9");

                    b.Property<string>("Status");

                    b.HasKey("QuestionsId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("ThesisReview.Data.Models.Report", b =>
                {
                    b.Property<int>("ReportId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int?>("FormId");

                    b.Property<string>("GradeGuardian");

                    b.Property<string>("GradeReviewer");

                    b.Property<string>("Guardian");

                    b.Property<string>("Reviewer");

                    b.Property<string>("Student");

                    b.HasKey("ReportId");

                    b.HasIndex("FormId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("ThesisReview.Data.Models.RequestForm", b =>
                {
                    b.Property<int>("RequestFormId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Department");

                    b.Property<string>("Email");

                    b.Property<string>("Fullname");

                    b.Property<string>("Title");

                    b.HasKey("RequestFormId");

                    b.ToTable("RequestForms");
                });

            modelBuilder.Entity("ThesisReview.Data.Models.UserList", b =>
                {
                    b.Property<string>("UserListId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("Department");

                    b.Property<string>("Fullname");

                    b.Property<string>("Mail");

                    b.Property<string>("Title");

                    b.HasKey("UserListId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("UserLists");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ThesisReview.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ThesisReview.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ThesisReview.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ThesisReview.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ThesisReview.Data.Models.Form", b =>
                {
                    b.HasOne("ThesisReview.Data.Models.UserList", "GuardianUser")
                        .WithMany()
                        .HasForeignKey("GuardianUserUserListId");

                    b.HasOne("ThesisReview.Data.Models.Questions", "QuestionsGuardian")
                        .WithMany()
                        .HasForeignKey("QuestionsGuardianQuestionsId");

                    b.HasOne("ThesisReview.Data.Models.Questions", "Questions")
                        .WithMany()
                        .HasForeignKey("QuestionsId");

                    b.HasOne("ThesisReview.Data.Models.UserList", "ReviewerUser")
                        .WithMany()
                        .HasForeignKey("ReviewerUserUserListId");
                });

            modelBuilder.Entity("ThesisReview.Data.Models.Report", b =>
                {
                    b.HasOne("ThesisReview.Data.Models.Form", "Form")
                        .WithMany()
                        .HasForeignKey("FormId");
                });

            modelBuilder.Entity("ThesisReview.Data.Models.UserList", b =>
                {
                    b.HasOne("ThesisReview.Data.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");
                });
#pragma warning restore 612, 618
        }
    }
}
