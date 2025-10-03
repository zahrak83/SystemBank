using SystemBank.Entities;
using SystemBank.Infrastructure;
using SystemBank.Interface.IRepository;

namespace SystemBank.Services
{
    public class CardRepository : ICardRepository
    {
        AppDbContext _context = new AppDbContext();

        public Card? GetCardByNumber(string cardNumber)
        {
            return _context.Cards.FirstOrDefault(c => c.CardNumber == cardNumber);
        }
        public List<Transaction> GetTransactions(string cardNumber)
        {
            return _context.Transactions
                .Where(t => t.SourceCardNumber == cardNumber || t.DestinationCardNumber == cardNumber)
                .OrderByDescending(t => t.TransactionDate)
                .ToList();
        }
        public void UpdateCard(Card updatedCard)
        {
            var card = _context.Cards.Find(updatedCard.CardNumber);

            if (card != null)
            {
                card.Balance = updatedCard.Balance;
                card.IsActive = updatedCard.IsActive;
                _context.SaveChanges();
            }
        }

    }
}
