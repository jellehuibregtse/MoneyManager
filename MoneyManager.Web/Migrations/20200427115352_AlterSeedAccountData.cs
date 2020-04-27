using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyManager.Web.Migrations
{
    public partial class AlterSeedAccountData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountId", "Balance", "Name" },
                values: new object[] { 2, 200m, "Account 2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: 2);
        }
    }
}
