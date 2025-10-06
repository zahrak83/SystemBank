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
            throw new NotImplementedException();
        }

        public Card GetCardByNumber(string cardNumber)
        {
            throw new NotImplementedException();
        }

        public Card GetCardWithTransactions(string cardNumber)
        {
            throw new NotImplementedException();
        }

        public void SetBalance(string cardNumber, float amount)
        {
            throw new NotImplementedException();
        }
    }
}
