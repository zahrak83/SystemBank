using SystemBank.Entities;

namespace SystemBank.Interface.IRepository
{
    public interface ICardRepository
    {
        bool CardExist(string cardNumber, string password);
        void SetBalance(string cardNumber, float amount);
        bool CardIsActive(string cardNumber);
        Card? GetCardByNumber(string cardNumber);
        float GetBalance(string cardNumber);
        Card? GetCardWithTransactions(string cardNumber);
        void SetDeactive(string cardNumber);
        public void SetActive(string cardNumber);
        void SetWrongPasswordTry(string cardNumber);
        void ClearWrongPasswordTry(string cardNumber);
        int GetWrongPasswordTry(string cardNumber);
        void SaveChanges();
        void SetPassword(string cardNumber, string newPassword);

    }
}
