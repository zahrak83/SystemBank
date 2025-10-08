using Microsoft.EntityFrameworkCore;
using SystemBank.Entities;
using SystemBank.Interface.IRepository;

namespace SystemBank.Infrastructure.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly AppDbContext _appDbContext;

        public CardRepository()
        {
            _appDbContext = new AppDbContext();
        }

        public bool CardExist(string cardNumber, string password)
        {
            return _appDbContext.Cards
                .Any(c => c.CardNumber == cardNumber && c.Password == password);
        }

        public bool CardIsActive(string cardNumber)
        {
            return _appDbContext.Cards
                .Any(c => c.CardNumber == cardNumber && c.IsActive);
        }

        public float GetBalance(string cardNumber)
        {
            return _appDbContext.Cards
                .Where(c => c.CardNumber == cardNumber)
                .Select(c => c.Balance)
                .First();
        }

        public Card GetCardByNumber(string cardNumber)
        {
            return _appDbContext.Cards
                .FirstOrDefault(c => c.CardNumber == cardNumber);
        }

        public Card GetCardWithTransactions(string cardNumber)
        {
            return _appDbContext.Cards
                .Include(c => c.TransactionsSource)
                .Include(c => c.TransactionsDestination)
                .First(c => c.CardNumber == cardNumber);
        }

        public void SetBalance(string cardNumber, float amount)
        {
            _appDbContext.Cards
                .Where(c => c.CardNumber == cardNumber)
                .ExecuteUpdate(setters => setters.SetProperty(c => c.Balance, amount));
        }
    }
}
