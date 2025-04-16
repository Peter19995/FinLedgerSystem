using System.ComponentModel.DataAnnotations;

namespace FinLedgerSystem.Models
{
    public class PurchaseInvoiceTransaction
    {
        public PurchaseInvoiceTransaction()
        {
            CreatedDate = DateTime.UtcNow;
            TransactionCode = GenerateTransactionCode();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string InvoiceNumber { get; set; }


        [Required]
        public string AssetAccountId { get; set; }

        public string InvoiceNotes { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        [Required]
        public float Amount { get; set; }

        public float TaxAmount { get; set; }

        public float Discount { get; set; }

        [Required]
        public float TotalAmount { get; set; }

        public int? CustomerId { get; set; }

        public int? SupplierId { get; set; }

        public bool IsPosted { get; set; } = false;

        public bool IsValid { get; set; } = true;

        [Required]
        public string TransactionCode { get; set; }

        public DateTime CreatedDate { get; set; }

        private string GenerateTransactionCode()
        {
            return $"TRX-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..6].ToUpper()}";
        }
    }
}
