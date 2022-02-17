using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JwtWebApiTutorial.Migrations
{
    public partial class NewModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo_name",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Is_late = table.Column<string>(type: "text", nullable: false),
                    Checkin_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Location_checkin = table.Column<string>(type: "text", nullable: false),
                    Description_checkin = table.Column<string>(type: "text", nullable: false),
                    Checkout_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Location_checkout = table.Column<string>(type: "text", nullable: false),
                    Description_checkout = table.Column<string>(type: "text", nullable: false),
                    Photo_name = table.Column<string>(type: "text", nullable: false),
                    Created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
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
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Start_checkin_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    End_checkin_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Start_checkout_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    End_checkout_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_UserId",
                table: "Attendances",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropColumn(
                name: "Photo_name",
                table: "Users");
        }
    }
}
