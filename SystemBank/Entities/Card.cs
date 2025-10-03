using System.ComponentModel.DataAnnotations;

namespace SystemBank.Entities
{
    public class Card
    {
        [Key]
        [MaxLength(16)]
        public string CardNumber { get; set; }
        public string HolderName { get; set; }
        public float Balance { get; set; }
        public bool IsActive { get; set; }

        [Required]
        public string Password { get; set; }
        public int FailedPasswordAttempts { get; set; } = 0;
        public List<Transaction> TransactionsSource { get; set; } = new();
        public List<Transaction> TransactionsDestination { get; set; } = new();

    }
}
