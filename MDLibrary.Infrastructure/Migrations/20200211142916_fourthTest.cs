using Microsoft.EntityFrameworkCore.Migrations;

namespace MDLibrary.Infrastructure.Migrations
{
    public partial class fourthTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BookCopy",
                columns: new[] { "ID", "BookDetailsID", "LoanID" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1,  },
                    { 3, 2, 2 },
                    { 4, 3,  }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookCopy",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookCopy",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookCopy",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BookCopy",
                keyColumn: "ID",
                keyValue: 4);
        }
    }
}
