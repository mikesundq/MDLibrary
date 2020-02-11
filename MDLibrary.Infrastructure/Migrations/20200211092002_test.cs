using Microsoft.EntityFrameworkCore.Migrations;

namespace MDLibrary.Infrastructure.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_BookDetails_BookDetailsID",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Loan_Book_BookCopyID",
                table: "Loan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Book",
                table: "Book");

            migrationBuilder.RenameTable(
                name: "Book",
                newName: "BookCopy");

            migrationBuilder.RenameIndex(
                name: "IX_Book_BookDetailsID",
                table: "BookCopy",
                newName: "IX_BookCopy_BookDetailsID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookCopy",
                table: "BookCopy",
                column: "ID");

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "ID",
                keyValue: 1,
                column: "SSN",
                value: "8004241234");

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "ID",
                keyValue: 2,
                column: "SSN",
                value: "8004191234");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCopy_BookDetails_BookDetailsID",
                table: "BookCopy",
                column: "BookDetailsID",
                principalTable: "BookDetails",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_BookCopy_BookCopyID",
                table: "Loan",
                column: "BookCopyID",
                principalTable: "BookCopy",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCopy_BookDetails_BookDetailsID",
                table: "BookCopy");

            migrationBuilder.DropForeignKey(
                name: "FK_Loan_BookCopy_BookCopyID",
                table: "Loan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookCopy",
                table: "BookCopy");

            migrationBuilder.RenameTable(
                name: "BookCopy",
                newName: "Book");

            migrationBuilder.RenameIndex(
                name: "IX_BookCopy_BookDetailsID",
                table: "Book",
                newName: "IX_Book_BookDetailsID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Book",
                table: "Book",
                column: "ID");

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "ID",
                keyValue: 1,
                column: "SSN",
                value: "800424-1234");

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "ID",
                keyValue: 2,
                column: "SSN",
                value: "800419-1234");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_BookDetails_BookDetailsID",
                table: "Book",
                column: "BookDetailsID",
                principalTable: "BookDetails",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_Book_BookCopyID",
                table: "Loan",
                column: "BookCopyID",
                principalTable: "Book",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
