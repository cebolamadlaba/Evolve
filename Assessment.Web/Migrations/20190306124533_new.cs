using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Assessment.Web.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalculationHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UploadTime = table.Column<DateTime>(nullable: false),
                    Size = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CompletedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculationHeaders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalculationResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CalculationHeaderId = table.Column<int>(nullable: false),
                    Formular = table.Column<string>(nullable: true),
                    InputA = table.Column<double>(nullable: false),
                    InputB = table.Column<double>(nullable: false),
                    InputC = table.Column<double>(nullable: false),
                    Result = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculationResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalculationResults_CalculationHeaders_CalculationHeaderId",
                        column: x => x.CalculationHeaderId,
                        principalTable: "CalculationHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalculationResults_CalculationHeaderId",
                table: "CalculationResults",
                column: "CalculationHeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculationResults");

            migrationBuilder.DropTable(
                name: "CalculationHeaders");
        }
    }
}
