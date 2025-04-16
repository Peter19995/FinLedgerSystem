using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinLedgerSystem.Models
{
    public class LedgerTransaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("LedgerAccount")]
        public int LedgerId { get; set; }

        public float DrAmount { get; set; } = 0;

        public float CrAmount { get; set; } = 0;

        [Required]
        public string TransactionType { get; set; }

        [Required]
        public string TransactionCode { get; set; }

        // ID of the original source (e.g., PurchaseInvoiceTransaction Id)
        [Required]
        public int SourceId { get; set; }

        public string TransactionReference { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Navigation property (optional)
        public LedgerAccount LedgerAccount { get; set; }
    }
}
