using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tkv.HealthInsurance.Domain.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coverage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternationalCode = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MinimumDeposit = table.Column<long>(type: "bigint", nullable: false),
                    MaximumDeposit = table.Column<long>(type: "bigint", nullable: false),
                    PriceMultiplier = table.Column<double>(type: "float", nullable: false),
                    InsertedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coverage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestLogMaster",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InsertedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestLogMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestLogDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestLogMasterId = table.Column<long>(type: "bigint", nullable: false),
                    CoverageId = table.Column<long>(type: "bigint", nullable: false),
                    DepositAmount = table.Column<long>(type: "bigint", nullable: false),
                    CalculatedValue = table.Column<double>(type: "float", nullable: false),
                    InsertedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestLogDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestLogDetail_Coverage_CoverageId",
                        column: x => x.CoverageId,
                        principalTable: "Coverage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestLogDetail_RequestLogMaster_RequestLogMasterId",
                        column: x => x.RequestLogMasterId,
                        principalTable: "RequestLogMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Coverage",
                columns: new[] { "Id", "DeletedDateTime", "InsertedDateTime", "InternationalCode", "LastModifiedDateTime", "MaximumDeposit", "MinimumDeposit", "PriceMultiplier", "Title" },
                values: new object[,]
                {
                    { 1L, null, new DateTime(2024, 9, 22, 15, 31, 34, 576, DateTimeKind.Local).AddTicks(5), 100, new DateTime(2024, 9, 22, 15, 31, 34, 576, DateTimeKind.Local).AddTicks(5), 500000000L, 5000L, 0.0051999999999999998, "Surgery" },
                    { 2L, null, new DateTime(2024, 9, 22, 15, 31, 34, 576, DateTimeKind.Local).AddTicks(5), 200, new DateTime(2024, 9, 22, 15, 31, 34, 576, DateTimeKind.Local).AddTicks(5), 400000000L, 4000L, 0.0041999999999999997, "Dentistry" },
                    { 3L, null, new DateTime(2024, 9, 22, 15, 31, 34, 576, DateTimeKind.Local).AddTicks(5), 300, new DateTime(2024, 9, 22, 15, 31, 34, 576, DateTimeKind.Local).AddTicks(5), 200000000L, 2000L, 0.0050000000000000001, "Hospitalization" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestLogDetail_CoverageId",
                table: "RequestLogDetail",
                column: "CoverageId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestLogDetail_RequestLogMasterId",
                table: "RequestLogDetail",
                column: "RequestLogMasterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestLogDetail");

            migrationBuilder.DropTable(
                name: "Coverage");

            migrationBuilder.DropTable(
                name: "RequestLogMaster");
        }
    }
}
