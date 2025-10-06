namespace SystemBank.Dtos
{
    public class GetTransactionDto
    {
        public int TransactionId { get; set; }
        public float Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool IsSuccessful { get; set; }
        public string SourceCardNumber { get; set; }
        public string DestinationCardNumber { get; set; }
    }
}
