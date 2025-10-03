using Microsoft.EntityFrameworkCore;
using SystemBank.Entities;

namespace SystemBank.Infrastructure
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-DG1LLR4\SQLEXPRESS;Database=Bank;Integrated Security=true;TrustServerCertificate=true;");
        }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>()
            .HasKey(c => c.CardNumber);

            modelBuilder.Entity<Card>()
                .Property(c => c.CardNumber)
                .HasMaxLength(16)
                .IsRequired();

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.SourceCard)
                .WithMany(c => c.TransactionsSource)
                .HasForeignKey(t => t.SourceCardNumber)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
           .HasOne(t => t.DestinationCard)
           .WithMany(c => c.TransactionsDestination)
           .HasForeignKey(t => t.DestinationCardNumber)
           .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
