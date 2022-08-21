using Microsoft.EntityFrameworkCore;

namespace SampleBankTransactions.Model
{
    public class BankTransactions : DbContext
    {
        public BankTransactions(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<Transaction>().ToTable("Transaction");
        }
    }
}
