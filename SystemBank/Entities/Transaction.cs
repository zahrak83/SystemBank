namespace SystemBank.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public Card SourceCard { get; set; }
        public string SourceCardNumber { get; set; }
        public Card DestinationCard { get; set; }
        public string DestinationCardNumber { get; set; }
        public float Amount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public bool IsSuccesful { get; set; }

    }
}
