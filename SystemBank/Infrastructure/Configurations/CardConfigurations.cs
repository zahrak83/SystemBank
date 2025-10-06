using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SystemBank.Entities;

namespace SystemBank.Infrastructure.Configurations
{
    public class CardConfigurations : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(c => c.CardNumber);

            builder.Property(c => c.CardNumber)
                .HasMaxLength(16)
                .IsRequired();

            builder.HasMany(c => c.TransactionsSource)
                .WithOne(t => t.SourceCard)
                .HasForeignKey(c => c.SourceCardNumber)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.TransactionsDestination)
                .WithOne(t => t.DestinationCard)
                .HasForeignKey(c => c.DestinationCardNumber)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
