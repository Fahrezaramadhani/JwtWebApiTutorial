using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JwtWebApiTutorial.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SettingName = table.Column<string>(type: "varchar(50)", nullable: false),
                    SettingValue = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    DeletedBy = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PositionName = table.Column<string>(type: "varchar(25)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    DeletedBy = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Religions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReligionName = table.Column<string>(type: "varchar(25)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    DeletedBy = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Religions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubmissionLeaves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateStart = table.Column<DateTime>(type: "Date", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "Date", nullable: false),
                    LeaveType = table.Column<string>(type: "varchar(20)", nullable: false),
                    SubmissionAttributeId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    DeletedBy = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionLeaves", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DatePerform = table.Column<DateTime>(type: "Date", nullable: false),
                    StartTime = table.Column<string>(type: "varchar(15)", nullable: false),
                    EndTime = table.Column<string>(type: "varchar(15)", nullable: false),
                    SubmissionType = table.Column<string>(type: "varchar(15)", nullable: false),
                    OvertimeId = table.Column<int>(type: "integer", nullable: false),
                    SubmissionAttributeId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    DeletedBy = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    ReligionId = table.Column<int>(type: "integer", nullable: false),
                    PositionId = table.Column<int>(type: "integer", nullable: false),
                    Gender = table.Column<string>(type: "varchar(10)", nullable: false),
                    Status = table.Column<string>(type: "varchar(25)", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "Date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "Date", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    Address = table.Column<string>(type: "varchar(150)", nullable: false),
                    City = table.Column<string>(type: "varchar(25)", nullable: false),
                    NoKTP = table.Column<string>(type: "varchar(20)", nullable: false),
                    NPWP = table.Column<string>(type: "varchar(20)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "Date", nullable: false),
                    Role = table.Column<string>(type: "varchar(20)", nullable: false),
                    Password = table.Column<string>(type: "varchar(250)", nullable: false),
                    PhotoName = table.Column<string>(type: "varchar(100)", nullable: false),
                    RefreshToken = table.Column<string>(type: "varchar(250)", nullable: false),
                    SuperiorId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    DeletedBy = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Religions_ReligionId",
                        column: x => x.ReligionId,
                        principalTable: "Religions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WhatTimeIs = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PhotoName = table.Column<string>(type: "varchar(100)", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "varchar(150)", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    DeletedBy = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityRecords_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsLate = table.Column<bool>(type: "boolean", nullable: false),
                    CheckinAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LocationCheckin = table.Column<string>(type: "varchar(150)", nullable: false),
                    DescriptionCheckin = table.Column<string>(type: "text", nullable: false),
                    CheckoutAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LocationCheckout = table.Column<string>(type: "varchar(150)", nullable: false),
                    DescriptionCheckout = table.Column<string>(type: "text", nullable: false),
                    PhotoName = table.Column<string>(type: "varchar(100)", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    DeletedBy = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendances_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubmissionAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateSubmit = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SubmissionStatus = table.Column<string>(type: "varchar(15)", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Attachment = table.Column<string>(type: "varchar(100)", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SubmissionId = table.Column<int>(type: "integer", nullable: true),
                    SubmissionLeaveId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    DeletedBy = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmissionAttributes_SubmissionLeaves_SubmissionLeaveId",
                        column: x => x.SubmissionLeaveId,
                        principalTable: "SubmissionLeaves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubmissionAttributes_Submissions_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "Submissions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubmissionAttributes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Approvals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusApproval = table.Column<string>(type: "varchar(15)", nullable: false),
                    DateApproval = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SubmissionAttributeId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    DeletedBy = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approvals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Approvals_SubmissionAttributes_SubmissionAttributeId",
                        column: x => x.SubmissionAttributeId,
                        principalTable: "SubmissionAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Approvals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityRecords_UserId",
                table: "ActivityRecords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Approvals_SubmissionAttributeId",
                table: "Approvals",
                column: "SubmissionAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Approvals_UserId",
                table: "Approvals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_UserId",
                table: "Attendances",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionAttributes_SubmissionId",
                table: "SubmissionAttributes",
                column: "SubmissionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionAttributes_SubmissionLeaveId",
                table: "SubmissionAttributes",
                column: "SubmissionLeaveId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionAttributes_UserId",
                table: "SubmissionAttributes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PositionId",
                table: "Users",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ReligionId",
                table: "Users",
                column: "ReligionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityRecords");

            migrationBuilder.DropTable(
                name: "ApplicationSettings");

            migrationBuilder.DropTable(
                name: "Approvals");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "SubmissionAttributes");

            migrationBuilder.DropTable(
                name: "SubmissionLeaves");

            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Religions");
        }
    }
}
