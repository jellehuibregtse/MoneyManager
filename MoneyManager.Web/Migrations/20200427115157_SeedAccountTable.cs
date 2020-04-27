using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyManager.Web.Migrations
{
    public partial class SeedAccountTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountId", "Balance", "Name" },
                values: new object[] { 1, 100m, "Account 1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: 1);
        }
    }
}
