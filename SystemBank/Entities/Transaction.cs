using System.ComponentModel.DataAnnotations;

namespace SystemBank.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        [MaxLength(16)]
        public string SourceCardNumber { get; set; }

        [MaxLength(16)]
        public string DestinationCardNumber { get; set; }
        public float Amount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public bool IsSuccesful { get; set; }
        public Card SourceCard { get; set; }
        public Card DestinationCard { get; set; }

    }
}
