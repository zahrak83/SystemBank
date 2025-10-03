using SystemBank.Entities;

namespace SystemBank.Interface.IRepository
{
    public interface ICardRepository
    {
        Card? GetCardByNumber(string cardNumber);
        void UpdateCard(Card card);
        List<Transaction> GetTransactions(string cardNumber);
    }
}
