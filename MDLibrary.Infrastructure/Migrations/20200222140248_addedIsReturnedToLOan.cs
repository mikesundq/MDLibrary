using Microsoft.EntityFrameworkCore.Migrations;

namespace MDLibrary.Infrastructure.Migrations
{
    public partial class addedIsReturnedToLOan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsReturned",
                table: "Loan",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "Loan");
        }
    }
}
