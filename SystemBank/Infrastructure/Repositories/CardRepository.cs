using SystemBank.Entities;
using SystemBank.Interface.IRepository;

namespace SystemBank.Infrastructure.Repositories
{
    public class CardRepository : ICardRepository
    {
        public bool CardExist(string cardNumber, string password)
        {
            throw new NotImplementedException();
        }

        public bool CardIsActive(string cardNumber)
        {
            throw new NotImplementedException();
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
