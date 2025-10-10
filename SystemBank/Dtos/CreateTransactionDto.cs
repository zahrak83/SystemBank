namespace SystemBank.Dtos
{
    public class CreateTransactionDto
    {
        public float Amount { get; set; }
        public float Fee { get; set; }
        public bool IsSuccessful { get; set; }
        public string SourceCardNumber { get; set; }
        public string DestinationCardNumber { get; set; }
    }
}
