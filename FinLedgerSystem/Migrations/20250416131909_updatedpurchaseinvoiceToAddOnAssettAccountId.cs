using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinLedgerSystem.Migrations
{
    /// <inheritdoc />
    public partial class updatedpurchaseinvoiceToAddOnAssettAccountId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssetAccountId",
                table: "PurchaseInvoiceTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssetAccountId",
                table: "PurchaseInvoiceTransactions");
        }
    }
}
