using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MossApp.Data.Migrations
{
    public partial class Intitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Options = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_Results", x => x.Id);
                });

            _ = migrationBuilder.CreateTable(
                name: "MatchPairs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlphaFileName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BetaFileName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LinesMatched = table.Column<int>(type: "int", nullable: false),
                    AlphaScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BetaScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AlphaPassage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BetaPassage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlphaLines = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    BetaLines = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ResultsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_MatchPairs", x => x.Id);
                    _ = table.ForeignKey(
                        name: "FK_MatchPairs_Results_ResultsId",
                        column: x => x.ResultsId,
                        principalTable: "Results",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            _ = migrationBuilder.InsertData(
                table: "Results",
                columns: new[] { "Id", "DateSubmitted", "Options" },
                values: new object[] { 1, new DateTime(2021, 10, 24, 17, 8, 10, 670, DateTimeKind.Local).AddTicks(1193), "example options" });

            _ = migrationBuilder.InsertData(
                table: "MatchPairs",
                columns: new[] { "Id", "AlphaFileName", "AlphaLines", "AlphaPassage", "AlphaScore", "BetaFileName", "BetaLines", "BetaPassage", "BetaScore", "LinesMatched", "ResultsId" },
                values: new object[] { 1, "AlphaFile", null, null, 66.6m, "BetaFile", "43 - 51", null, 33.3m, 45, 1 });

            _ = migrationBuilder.CreateIndex(
                name: "IX_MatchPairs_ResultId",
                table: "MatchPairs",
                column: "Id");

            _ = migrationBuilder.CreateIndex(
                name: "IX_MatchPairs_ResultsId",
                table: "MatchPairs",
                column: "ResultsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropTable(
                name: "MatchPairs");

            _ = migrationBuilder.DropTable(
                name: "Results");
        }
    }
}
