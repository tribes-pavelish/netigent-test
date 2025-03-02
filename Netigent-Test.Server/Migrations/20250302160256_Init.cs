using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Netigent_Test.Server.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpenDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompleteDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjectValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inquiries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: true),
                    SendToPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendToRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendToPersonId = table.Column<int>(type: "int", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inquiry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AskedDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedDt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inquiries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusLevels", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "StatusLevels",
                columns: new[] { "Id", "StatusName" },
                values: new object[,]
                {
                    { 1, "Approved" },
                    { 2, "In-Progress" },
                    { 3, "Paused" },
                    { 4, "Completed" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Inquiries");

            migrationBuilder.DropTable(
                name: "StatusLevels");
        }
    }
}
