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
    [Migration("20211206131716_AddedFPToStudent")]
    partial class AddedFPToStudent
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci")
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("PracticeJob.DAL.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<int>("ProvinceId")
                        .HasColumnType("int");

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
                        .HasColumnType("longtext");

                    b.Property<int>("FPId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("ProfileImage")
                        .HasColumnType("longtext");

                    b.Property<int>("ProvinceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FPId");

                    b.HasIndex("ProvinceId");

                    b.ToTable("Students");
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
#pragma warning restore 612, 618
        }
    }
}
