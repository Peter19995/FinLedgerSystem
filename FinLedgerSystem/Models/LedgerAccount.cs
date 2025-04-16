namespace FinLedgerSystem.Models
{
    public class LedgerAccount
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; } // e.g. "120010"
        public string AccountName { get; set; } // e.g. "Accounts Receivable"
        public AccountType Type { get; set; } // Asset, Liability, Income, Expense
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public enum AccountType
    {
        Asset,
        Liability,
        Income,
        Expense,
        Equity,
        Revenue
    }
}
