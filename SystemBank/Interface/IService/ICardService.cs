using SystemBank.Entities;

namespace SystemBank.Interface.IService
{
    public interface ICardService
    {
        Card Authenticate(string cardNumber, string password);
        Card Transfer(string sourceCardNumber, string destinationCardNumber, float amount);
        List<Transaction> GetTransactions(string cardNumber);
    }
}

