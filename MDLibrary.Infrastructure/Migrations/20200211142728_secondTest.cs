using Microsoft.EntityFrameworkCore.Migrations;

namespace MDLibrary.Infrastructure.Migrations
{
    public partial class secondTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Thomas Årnfelt" },
                    { 2, "Johan Hagesund" }
                });

            migrationBuilder.InsertData(
                table: "Member",
                columns: new[] { "ID", "Name", "SSN" },
                values: new object[,]
                {
                    { 1, "Mikael Sundqvist", "8004241234" },
                    { 2, "Daniel Ny", "8004191234" }
                });

            migrationBuilder.InsertData(
                table: "BookDetails",
                columns: new[] { "ID", "AuthorID", "Details", "ISBN", "Titel" },
                values: new object[] { 1, 1, "Incidenten i Böhmen är Linköpingsförfattaren Thomas Årnfelts debutroman. I den blandas historia med vidskepelse och ockultism på ett sätt som passar den tidigmoderna världen före upplysningen.", "9789198138795", "Incidenten i Böhmen" });

            migrationBuilder.InsertData(
                table: "BookDetails",
                columns: new[] { "ID", "AuthorID", "Details", "ISBN", "Titel" },
                values: new object[] { 3, 1, "Den som söker är en psykologisk spänningsroman där Johan följer tips som leder honom till makabra brottsplatser och ger hans karriär en skjuts framåt. Men vad är det egentligen som händer och vem är det som tipsar? Vilka mörka krafter är det som har satts i rörelse? Är det verkligen ok att gå över lik för att nå sina drömmars mål?", "9789198428506", "Den som söker" });

            migrationBuilder.InsertData(
                table: "BookDetails",
                columns: new[] { "ID", "AuthorID", "Details", "ISBN", "Titel" },
                values: new object[] { 2, 2, "Historien om Linköpings Hockey Club börjar inte den 4 augusti 1976. LHC bildades visserligen den dagen men spelartruppen, utrustningen, platsen i seriesystemet och traditionen var densamma som i BK Kenty som man bröt sig ut från.", "9789198075526", "Linköpings Hockey Club och den förändrade självbilden" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookDetails",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookDetails",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookDetails",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "ID",
                keyValue: 2);
        }
    }
}
