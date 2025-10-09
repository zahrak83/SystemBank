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
                .FirstOrDefault();
        }
        public Card? GetCardByNumber(string cardNumber)
        {
            return _appDbContext.Cards
                .FirstOrDefault(c => c.CardNumber == cardNumber);
        }
        public Card? GetCardWithTransactions(string cardNumber)
        {
            return _appDbContext.Cards
                .Include(c => c.TransactionsSource)
                .Include(c => c.TransactionsDestination)
                .FirstOrDefault(c => c.CardNumber == cardNumber);
        }
        public void SetBalance(string cardNumber, float amount)
        {
            var card = _appDbContext.Cards.First(c => c.CardNumber == cardNumber);
            card.Balance = amount;
        }
        public int GetWrongPasswordTry(string cardNumber)
        {
            return _appDbContext.Cards
                .Where(c => c.CardNumber == cardNumber)
                .Select(c => c.FailedPasswordAttempts)
                .First();
        }
        public void ClearWrongPasswordTry(string cardNumber)
        {
            _appDbContext.Cards
                .Where(c => c.CardNumber == cardNumber)
                .ExecuteUpdate(setters => setters
                .SetProperty(c => c.FailedPasswordAttempts, 0));
        }
        public void SetWrongPasswordTry(string cardNumber)
        {
            _appDbContext.Cards
                .Where(c => c.CardNumber == cardNumber)
                .ExecuteUpdate(setters => setters
                .SetProperty(c => c.FailedPasswordAttempts, c => c.FailedPasswordAttempts + 1));
        }
        public void SetActive(string cardNumber)
        {
            _appDbContext.Cards
                .Where(c => c.CardNumber == cardNumber)
                .ExecuteUpdate(setters => setters
                .SetProperty(c => c.IsActive, true));
        }
        public void SetDeactive(string cardNumber)
        {
            _appDbContext.Cards
                .Where(c => c.CardNumber == cardNumber)
                .ExecuteUpdate(setters => setters
                .SetProperty(c => c.IsActive, false));
        }
        public void SaveChanges()
        {
            _appDbContext.SaveChanges();
        }
    }
}
