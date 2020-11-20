using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Domain.Data
{
    public class ExpenseDbContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }

        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>().ToTable("Expense").HasKey(e => e.Id);
        }
    }
}
