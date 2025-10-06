using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SystemBank.Entities;

namespace SystemBank.Infrastructure.Configurations
{
    public class TransactionConfigurations : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(t => t.TransactionId);

            builder.Property(t => t.SourceCardNumber).HasMaxLength(16);

            builder.Property(t => t.DestinationCardNumber).HasMaxLength(16);
        }
    }
}
