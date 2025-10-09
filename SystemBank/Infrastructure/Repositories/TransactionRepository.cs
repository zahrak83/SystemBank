using SystemBank.Entities;
using SystemBank.Dtos;
using SystemBank.Interface.IRepository;

namespace SystemBank.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _appDbContext;

        public TransactionRepository()
        {
            _appDbContext = new AppDbContext();
        }
        public void Create(CreateTransactionDto model)
        {
            var entity = new Transaction()
            {
                DestinationCardNumber = model.DestinationCardNumber,
                SourceCardNumber = model.SourceCardNumber,
                IsSuccesful = model.IsSuccessful,
                TransactionDate = DateTime.Now,
                Amount = model.Amount,
            };

            _appDbContext.Transactions.Add(entity);
            _appDbContext.SaveChanges();
        }
        public float DailyWithdrawal(string cardNumber)
        {
            var amountOfTransactions = _appDbContext.Transactions
                    .Where(x => x.TransactionDate.Date == DateTime.Now.Date && x.SourceCard.CardNumber == cardNumber)
                    .Sum(x => x.Amount);

            return amountOfTransactions;
        }
        public List<GetTransactionDto> GetAll(string cardNumber)
        {
            return _appDbContext.Transactions
                .Where(x => x.SourceCardNumber == cardNumber ||
                 x.DestinationCardNumber == cardNumber)
                 .Select(t => new GetTransactionDto
                 {
                     TransactionId = t.TransactionId,
                     Amount = t.Amount,
                     DestinationCardNumber = t.DestinationCardNumber,
                     IsSuccessful = t.IsSuccesful,
                     SourceCardNumber = t.SourceCardNumber,
                     TransactionDate = t.TransactionDate
                 }).ToList();

        }
    }
}
