﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PracticeJob.DAL.Entities;

namespace PracticeJob.DAL.Migrations
{
    [DbContext(typeof(PracticeJobContext))]
    [Migration("20220222011420_PrivateMessagesAdded")]
    partial class PrivateMessagesAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci")
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("FPJobOffer", b =>
                {
                    b.Property<int>("FPsId")
                        .HasColumnType("int");

                    b.Property<int>("JobOffersId")
                        .HasColumnType("int");

                    b.HasKey("FPsId", "JobOffersId");

                    b.HasIndex("JobOffersId");

                    b.ToTable("FPJobOffer");
                });

            modelBuilder.Entity("PracticeJob.DAL.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("ProfileImage")
                        .HasColumnType("longtext");

                    b.Property<int>("ProvinceId")
                        .HasColumnType("int");

                    b.Property<string>("StripeId")
                        .HasColumnType("longtext");

                    b.Property<string>("TFCode")
                        .HasColumnType("longtext");

                    b.Property<bool>("ValidatedEmail")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("ProvinceId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("PracticeJob.DAL.Entities.FP", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("FPFamilyId")
                        .HasColumnType("int");

                    b.Property<int>("FPGradeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("FPFamilyId");

                    b.HasIndex("FPGradeId");

                    b.ToTable("FPs");
                });

            modelBuilder.Entity("PracticeJob.DAL.Entities.FPFamily", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("FPFamilies");
                });

            modelBuilder.Entity("PracticeJob.DAL.Entities.FPGrade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("FPGrades");
                });

            modelBuilder.Entity("PracticeJob.DAL.Entities.JobApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("ApplicationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ApplicationStatus")
                        .HasColumnType("int");

                    b.Property<int>("JobOfferId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("JobOfferId");

                    b.HasIndex("StudentId");

                    b.ToTable("JobApllications");
                });

            modelBuilder.Entity("PracticeJob.DAL.Entities.JobOffer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Remuneration")
                        .HasColumnType("int");

                    b.Property<string>("Schedule")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("JobOffers");
                });

            modelBuilder.Entity("PracticeJob.DAL.Entities.PrivateMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasColumnType("longtext");

                    b.Property<bool>("Read")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("StudentId");

                    b.ToTable("PrivateMessages");
                });

            modelBuilder.Entity("PracticeJob.DAL.Entities.Province", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("PracticeJob.DAL.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("Date");

                    b.Property<string>("City")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("FPCalification")
                        .HasColumnType("double");

                    b.Property<int>("FPId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ProfileImage")
                        .HasColumnType("longtext");

                    b.Property<int>("ProvinceId")
                        .HasColumnType("int");

                    b.Property<string>("TFCode")
                        .HasColumnType("longtext");

                    b.Property<bool>("ValidatedEmail")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("FPId");

                    b.HasIndex("ProvinceId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("FPJobOffer", b =>
                {
                    b.HasOne("PracticeJob.DAL.Entities.FP", null)
                        .WithMany()
                        .HasForeignKey("FPsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PracticeJob.DAL.Entities.JobOffer", null)
                        .WithMany()
                        .HasForeignKey("JobOffersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PracticeJob.DAL.Entities.Company", b =>
                {
                    b.HasOne("PracticeJob.DAL.Entities.Province", "Province")
                        .WithMany()
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Province");
                });

            modelBuilder.Entity("PracticeJob.DAL.Entities.FP", b =>
                {
                    b.HasOne("PracticeJob.DAL.Entities.FPFamily", "FPFamily")
                        .WithMany()
                        .HasForeignKey("FPFamilyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PracticeJob.DAL.Entities.FPGrade", "FPGrade")
                        .WithMany()
                        .HasForeignKey("FPGradeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FPFamily");

                    b.Navigation("FPGrade");
                });

            modelBuilder.Entity("PracticeJob.DAL.Entities.JobApplication", b =>
                {
                    b.HasOne("PracticeJob.DAL.Entities.JobOffer", "JobOffer")
                        .WithMany("JobApplications")
                        .HasForeignKey("JobOfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PracticeJob.DAL.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobOffer");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("PracticeJob.DAL.Entities.JobOffer", b =>
                {
                    b.HasOne("PracticeJob.DAL.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("PracticeJob.DAL.Entities.PrivateMessage", b =>
                {
                    b.HasOne("PracticeJob.DAL.Entities.Company", "Company")
                        .WithMany("PrivateMessages")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PracticeJob.DAL.Entities.Student", "Student")
                        .WithMany("PrivateMessages")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("PracticeJob.DAL.Entities.Student", b =>
                {
                    b.HasOne("PracticeJob.DAL.Entities.FP", "FP")
                        .WithMany()
                        .HasForeignKey("FPId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PracticeJob.DAL.Entities.Province", "Province")
                        .WithMany()
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FP");

                    b.Navigation("Province");
                });

            modelBuilder.Entity("PracticeJob.DAL.Entities.Company", b =>
                {
                    b.Navigation("PrivateMessages");
                });

            modelBuilder.Entity("PracticeJob.DAL.Entities.JobOffer", b =>
                {
                    b.Navigation("JobApplications");
                });

            modelBuilder.Entity("PracticeJob.DAL.Entities.Student", b =>
                {
                    b.Navigation("PrivateMessages");
                });
#pragma warning restore 612, 618
        }
    }
}
