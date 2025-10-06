using SystemBank.Entities;

namespace SystemBank.Interface.IRepository
{
    public interface ICardRepository
    {
        bool CardExist(string cardNumber, string password);
        void SetBalance(string cardNumber, float amount);
        bool CardIsActive(string cardNumber);
        Card GetCardByNumber(string cardNumber);
        float GetBalance(string cardNumber);
        Card GetCardWithTransactions(string cardNumber);
    }
}
