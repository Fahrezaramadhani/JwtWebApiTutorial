﻿// <auto-generated />
using System;
using JwtWebApiTutorial.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JwtWebApiTutorial.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220625140227_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("JwtWebApiTutorial.Models.ActivityRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("PhotoName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("WhatTimeIs")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ActivityRecords");
                });

            modelBuilder.Entity("JwtWebApiTutorial.Models.ApplicationSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("SettingName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("SettingValue")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationSettings");
                });

            modelBuilder.Entity("JwtWebApiTutorial.Models.Approval", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("DateApproval")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("StatusApproval")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<int>("SubmissionAttributeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SubmissionAttributeId");

                    b.HasIndex("UserId");

                    b.ToTable("Approvals");
                });

            modelBuilder.Entity("JwtWebApiTutorial.Models.Attendance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CheckinAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CheckoutAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("DescriptionCheckin")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DescriptionCheckout")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsLate")
                        .HasColumnType("boolean");

                    b.Property<string>("LocationCheckin")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("LocationCheckout")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("PhotoName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("JwtWebApiTutorial.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PositionName")
                        .IsRequired()
                        .HasColumnType("varchar(25)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("JwtWebApiTutorial.Models.Religion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ReligionName")
                        .IsRequired()
                        .HasColumnType("varchar(25)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Religions");
                });

            modelBuilder.Entity("JwtWebApiTutorial.Models.Submission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("DatePerform")
                        .HasColumnType("Date");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("EndTime")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<int>("OvertimeId")
                        .HasColumnType("integer");

                    b.Property<string>("StartTime")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<int>("SubmissionAttributeId")
                        .HasColumnType("integer");

                    b.Property<string>("SubmissionType")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Submissions");
                });

            modelBuilder.Entity("JwtWebApiTutorial.Models.SubmissionAttribute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Attachment")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("DateSubmit")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("SubmissionId")
                        .HasColumnType("integer");

                    b.Property<int?>("SubmissionLeaveId")
                        .HasColumnType("integer");

                    b.Property<string>("SubmissionStatus")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SubmissionId")
                        .IsUnique();

                    b.HasIndex("SubmissionLeaveId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("SubmissionAttributes");
                });

            modelBuilder.Entity("JwtWebApiTutorial.Models.SubmissionLeave", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("Date");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("Date");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LeaveType")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<int>("SubmissionAttributeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("SubmissionLeaves");
                });

            modelBuilder.Entity("JwtWebApiTutorial.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("varchar(25)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("Date");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("Date");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("Date");

                    b.Property<string>("NPWP")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("NoKTP")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("PhotoName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("PositionId")
                        .HasColumnType("integer");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<int>("ReligionId")
                        .HasColumnType("integer");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(25)");

                    b.Property<int>("SuperiorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.HasIndex("ReligionId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("JwtWebApiTutorial.Models.ActivityRecord", b =>
                {
                    b.HasOne("JwtWebApiTutorial.Models.User", "User")
                        .WithMany("ActivityRecords")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("JwtWebApiTutorial.Models.Approval", b =>
                {
                    b.HasOne("JwtWebApiTutorial.Models.SubmissionAttribute", "SubmissionAttribute")
                        .WithMany("Approvals")
                        .HasForeignKey("SubmissionAttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JwtWebApiTutorial.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubmissionAttribute");

                    b.Navigation("User");
                });

            modelBuilder.Entity("JwtWebApiTutorial.Models.Attendance", b =>
                {
                    b.HasOne("JwtWebApiTutorial.Models.User", "User")
                        .WithMany("Attendances")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("JwtWebApiTutorial.Models.SubmissionAttribute", b =>
                {
                    b.HasOne("JwtWebApiTutorial.Models.Submission", "Submission")
                        .WithOne("SubmissionAttribute")
                        .HasForeignKey("JwtWebApiTutorial.Models.SubmissionAttribute", "SubmissionId");

                    b.HasOne("JwtWebApiTutorial.Models.SubmissionLeave", "SubmissionLeave")
                        .WithOne("SubmissionAttribute")
                        .HasForeignKey("JwtWebApiTutorial.Models.SubmissionAttribute", "SubmissionLeaveId");

                    b.HasOne("JwtWebApiTutorial.Models.User", "User")
                        .WithMany("SubmissionAttributes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Submission");

                    b.Navigation("SubmissionLeave");

                    b.Navigation("User");
                });

            modelBuilder.Entity("JwtWebApiTutorial.Models.User", b =>
                {
                    b.HasOne("JwtWebApiTutorial.Models.Position", "Position")
                        .WithMany("Users")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JwtWebApiTutorial.Models.Religion", "Religion")
                        .WithMany("Users")
                        .HasForeignKey("ReligionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Position");

                    b.Navigation("Religion");
                });

            modelBuilder.Entity("JwtWebApiTutorial.Models.Position", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("JwtWebApiTutorial.Models.Religion", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("JwtWebApiTutorial.Models.Submission", b =>
                {
                    b.Navigation("SubmissionAttribute")
                        .IsRequired();
                });

            modelBuilder.Entity("JwtWebApiTutorial.Models.SubmissionAttribute", b =>
                {
                    b.Navigation("Approvals");
                });

            modelBuilder.Entity("JwtWebApiTutorial.Models.SubmissionLeave", b =>
                {
                    b.Navigation("SubmissionAttribute")
                        .IsRequired();
                });

            modelBuilder.Entity("JwtWebApiTutorial.Models.User", b =>
                {
                    b.Navigation("ActivityRecords");

                    b.Navigation("Attendances");

                    b.Navigation("SubmissionAttributes");
                });
#pragma warning restore 612, 618
        }
    }
}