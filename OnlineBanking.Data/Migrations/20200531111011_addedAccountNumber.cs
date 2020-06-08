using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineBanking.Data.Migrations
{
    public partial class addedAccountNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Account",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Account");
        }
    }
}
