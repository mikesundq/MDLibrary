using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MDLibrary.Infrastructure.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SSN = table.Column<string>(maxLength: 12, nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BookDetails",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ISBN = table.Column<string>(maxLength: 13, nullable: true),
                    Titel = table.Column<string>(nullable: true),
                    AuthorID = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookDetails_Author_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Author",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookDetailsID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Book_BookDetails_BookDetailsID",
                        column: x => x.BookDetailsID,
                        principalTable: "BookDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loan",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeOfLoan = table.Column<DateTime>(nullable: false),
                    TimeToReturnBook = table.Column<DateTime>(nullable: false),
                    BookCopyID = table.Column<int>(nullable: false),
                    MemberID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Loan_Book_BookCopyID",
                        column: x => x.BookCopyID,
                        principalTable: "Book",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loan_Member_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_BookDetailsID",
                table: "Book",
                column: "BookDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_BookDetails_AuthorID",
                table: "BookDetails",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_BookCopyID",
                table: "Loan",
                column: "BookCopyID");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_MemberID",
                table: "Loan",
                column: "MemberID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loan");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "BookDetails");

            migrationBuilder.DropTable(
                name: "Author");
        }
    }
}
