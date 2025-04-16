using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinLedgerSystem.Migrations
{
    /// <inheritdoc />
    public partial class updatedPurcahseInvoiceToAddNotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvoiceNotes",
                table: "PurchaseInvoiceTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceNotes",
                table: "PurchaseInvoiceTransactions");
        }
    }
}
