using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyManager.DAL.Migrations
{
    public partial class Updated_CategoryModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Transaction_TransactionId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_TransactionId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Account");

            migrationBuilder.AlterColumn<int>(
                name: "TransactionId",
                table: "Category",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Category_TransactionId",
                table: "Category",
                column: "TransactionId",
                unique: true,
                filter: "[TransactionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Transaction_TransactionId",
                table: "Category",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Transaction_TransactionId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_TransactionId",
                table: "Category");

            migrationBuilder.AlterColumn<int>(
                name: "TransactionId",
                table: "Category",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "Account",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Category_TransactionId",
                table: "Category",
                column: "TransactionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Transaction_TransactionId",
                table: "Category",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
