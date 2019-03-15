using Microsoft.EntityFrameworkCore.Migrations;

namespace Assessment.Web.Migrations
{
    public partial class addMessageProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "CalculationResults",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "CalculationResults");
        }
    }
}
