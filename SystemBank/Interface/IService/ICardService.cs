using SystemBank.Entities;

namespace SystemBank.Interface.IService
{
    public interface ICardService
    {
        void Authenticate(string cardNumber, string password);
        void Transfer(string sourceCardNumber, string destinationCardNumber, float amount);
        List<Transaction> GetTransactions(string cardNumber);
    }
}

