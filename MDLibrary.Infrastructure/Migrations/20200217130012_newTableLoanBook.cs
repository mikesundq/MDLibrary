using Microsoft.EntityFrameworkCore.Migrations;

namespace MDLibrary.Infrastructure.Migrations
{
    public partial class newTableLoanBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCopy_Loan_LoanID",
                table: "BookCopy");

            migrationBuilder.AlterColumn<int>(
                name: "LoanID",
                table: "BookCopy",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "LoanBook",
                columns: table => new
                {
                    BookCopyID = table.Column<int>(nullable: false),
                    LoanID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanBook", x => new { x.LoanID, x.BookCopyID });
                    table.ForeignKey(
                        name: "FK_LoanBook_BookCopy_BookCopyID",
                        column: x => x.BookCopyID,
                        principalTable: "BookCopy",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanBook_Loan_LoanID",
                        column: x => x.LoanID,
                        principalTable: "Loan",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "BookCopy",
                keyColumn: "ID",
                keyValue: 1,
                column: "LoanID",
                value: null);

            migrationBuilder.UpdateData(
                table: "BookCopy",
                keyColumn: "ID",
                keyValue: 2,
                column: "LoanID",
                value: null);

            migrationBuilder.UpdateData(
                table: "BookCopy",
                keyColumn: "ID",
                keyValue: 3,
                column: "LoanID",
                value: null);

            migrationBuilder.UpdateData(
                table: "BookCopy",
                keyColumn: "ID",
                keyValue: 4,
                column: "LoanID",
                value: null);

            migrationBuilder.InsertData(
                table: "LoanBook",
                columns: new[] { "LoanID", "BookCopyID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 3 },
                    { 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanBook_BookCopyID",
                table: "LoanBook",
                column: "BookCopyID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCopy_Loan_LoanID",
                table: "BookCopy",
                column: "LoanID",
                principalTable: "Loan",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCopy_Loan_LoanID",
                table: "BookCopy");

            migrationBuilder.DropTable(
                name: "LoanBook");

            migrationBuilder.AlterColumn<int>(
                name: "LoanID",
                table: "BookCopy",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "BookCopy",
                keyColumn: "ID",
                keyValue: 1,
                column: "LoanID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "BookCopy",
                keyColumn: "ID",
                keyValue: 2,
                column: "LoanID",
                value: 0);

            migrationBuilder.UpdateData(
                table: "BookCopy",
                keyColumn: "ID",
                keyValue: 3,
                column: "LoanID",
                value: 2);

            migrationBuilder.UpdateData(
                table: "BookCopy",
                keyColumn: "ID",
                keyValue: 4,
                column: "LoanID",
                value: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_BookCopy_Loan_LoanID",
                table: "BookCopy",
                column: "LoanID",
                principalTable: "Loan",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
