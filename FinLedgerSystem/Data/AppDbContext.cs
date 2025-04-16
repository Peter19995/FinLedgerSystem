using FinLedgerSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FinLedgerSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<LedgerAccount> LedgerAccounts { get; set; }
        public DbSet<PurchaseInvoiceTransaction> PurchaseInvoiceTransactions { get; set; }
        public DbSet<LedgerTransaction> LedgerTransactions { get; set; }


    }
}
