using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MDLibrary.Infrastructure.Migrations
{
    public partial class someloans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Loan",
                columns: new[] { "ID", "BookCopyID", "MemberID", "TimeOfLoan", "TimeToReturnBook" },
                values: new object[] { 1, 2, 1, new DateTime(2020, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Loan",
                columns: new[] { "ID", "BookCopyID", "MemberID", "TimeOfLoan", "TimeToReturnBook" },
                values: new object[] { 2, 3, 2, new DateTime(2021, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Loan",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Loan",
                keyColumn: "ID",
                keyValue: 2);
        }
    }
}
