namespace FinLedgerSystem.Models.DTO
{
    public class PurchaseInvoiceTransactionDTO
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } // e.g. "INV-001"
        public string AssetAccountId { get; set; }
        public string InvoiceNotes { get; set; }
        public DateTime InvoiceDate { get; set; } // e.g. 2023-10-01
        public float Amount { get; set; } // e.g. 1000.00
        public float TaxAmount { get; set; } // e.g. 100.00
        public float Discount { get; set; } // e.g. 50.00
        public float TotalAmount { get; set; } // e.g. 1050.00
        public int? CustomerId { get; set; } // e.g. 1
        public int? SupplierId { get; set; } // e.g. 2
        public bool IsPosted { get; set; } = false;
    }
}
