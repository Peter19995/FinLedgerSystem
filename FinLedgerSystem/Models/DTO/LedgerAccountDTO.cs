namespace FinLedgerSystem.Models.DTO
{
    public class LedgerAccountDTO
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; } // e.g. "120010"
        public string AccountName { get; set; } // e.g. "Accounts Receivable"
        public string Type { get; set; } // Asset, Liability, Income, Expense
        public bool IsActive { get; set; }
    }
}
