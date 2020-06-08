using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineBanking.Data.Migrations
{
    public partial class descriptionproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Transaction",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Transaction");
        }
    }
}
