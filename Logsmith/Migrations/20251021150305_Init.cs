using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logsmith.Api.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EventType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Device = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    LatencyMs = table.Column<int>(type: "int", nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "DATEADD(HOUR, 9, SYSUTCDATETIME())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLog", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventLog_CreatedDate",
                table: "EventLog",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_EventLog_EventType_CreatedDate",
                table: "EventLog",
                columns: new[] { "EventType", "CreatedDate" });

            migrationBuilder.CreateIndex(
                name: "IX_EventLog_UserId_CreatedDate",
                table: "EventLog",
                columns: new[] { "UserId", "CreatedDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventLog");
        }
    }
}
