using Microsoft.EntityFrameworkCore;
using SystemBank.Entities;
using SystemBank.Infrastructure.Configurations;

namespace SystemBank.Infrastructure
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-DG1LLR4\SQLEXPRESS;Database=Bank;Integrated Security=true;TrustServerCertificate=true;");

            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CardConfigurations());
            modelBuilder.ApplyConfiguration(new TransactionConfigurations());

            base.OnModelCreating(modelBuilder);
        }

    }
}
