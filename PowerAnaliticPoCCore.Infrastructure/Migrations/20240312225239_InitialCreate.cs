using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PowerAnaliticPoCCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PowerGeneratorDetailData",
                columns: table => new
                {
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneratorId = table.Column<int>(type: "int", nullable: false),
                    CurrentProduction = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerGeneratorDetailData", x => new { x.TimeStamp, x.GeneratorId });
                });

            migrationBuilder.CreateTable(
                name: "PowerGenerators",
                columns: table => new
                {
                    GeneratorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ExpectedCurrent = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerGenerators", x => x.GeneratorId);
                });

            migrationBuilder.CreateTable(
                name: "PowerGeneratorTimeRangeData",
                columns: table => new
                {
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeRange = table.Column<int>(type: "int", nullable: false),
                    GeneratorId = table.Column<int>(type: "int", nullable: false),
                    CurrentProduction = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerGeneratorTimeRangeData", x => new { x.TimeStamp, x.TimeRange, x.GeneratorId });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PowerGeneratorDetailData");

            migrationBuilder.DropTable(
                name: "PowerGenerators");

            migrationBuilder.DropTable(
                name: "PowerGeneratorTimeRangeData");
        }
    }
}
