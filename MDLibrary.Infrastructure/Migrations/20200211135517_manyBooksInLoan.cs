using Microsoft.EntityFrameworkCore.Migrations;

namespace MDLibrary.Infrastructure.Migrations
{
    public partial class manyBooksInLoan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loan_BookCopy_BookCopyID",
                table: "Loan");

            migrationBuilder.DropIndex(
                name: "IX_Loan_BookCopyID",
                table: "Loan");

            migrationBuilder.DropColumn(
                name: "BookCopyID",
                table: "Loan");

            migrationBuilder.AddColumn<int>(
                name: "LoanID",
                table: "BookCopy",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "BookCopy",
                keyColumn: "ID",
                keyValue: 1,
                column: "LoanID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "BookCopy",
                keyColumn: "ID",
                keyValue: 3,
                column: "LoanID",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_BookCopy_LoanID",
                table: "BookCopy",
                column: "LoanID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCopy_Loan_LoanID",
                table: "BookCopy",
                column: "LoanID",
                principalTable: "Loan",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCopy_Loan_LoanID",
                table: "BookCopy");

            migrationBuilder.DropIndex(
                name: "IX_BookCopy_LoanID",
                table: "BookCopy");

            migrationBuilder.DropColumn(
                name: "LoanID",
                table: "BookCopy");

            migrationBuilder.AddColumn<int>(
                name: "BookCopyID",
                table: "Loan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Loan",
                keyColumn: "ID",
                keyValue: 1,
                column: "BookCopyID",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Loan",
                keyColumn: "ID",
                keyValue: 2,
                column: "BookCopyID",
                value: 3);

            migrationBuilder.CreateIndex(
                name: "IX_Loan_BookCopyID",
                table: "Loan",
                column: "BookCopyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_BookCopy_BookCopyID",
                table: "Loan",
                column: "BookCopyID",
                principalTable: "BookCopy",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
