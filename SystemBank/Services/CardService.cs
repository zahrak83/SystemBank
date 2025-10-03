using SystemBank.Entities;
using SystemBank.Infrastructure;
using SystemBank.Interface.IService;

namespace SystemBank.Services
{
    public class CardService : ICardService
    {
        private readonly CardRepository _repository = new CardRepository();

        public void Authenticate(string cardNumber, string password)
        {
            if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length != 16)
            {
                Console.WriteLine("Card number must be exactly 16 digits.");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Password cannot be empty.");
                return;
            }

            var card = _repository.GetCardByNumber(cardNumber);
            if (card == null)
            {
                Console.WriteLine("Card not found.");
                return;
            }

            if (!card.IsActive)
            {
                Console.WriteLine("This card is blocked or not active.");
                return;
            }

            if (card.Password != password)
            {
                card.FailedPasswordAttempts++;

                if (card.FailedPasswordAttempts >= 3)
                {
                    card.IsActive = false;
                    _repository.UpdateCard(card);
                    Console.WriteLine("Your card was blocked due to 3 unsuccessful attempts.");
                    return;
                }

                _repository.UpdateCard(card);
                Console.WriteLine($"Incorrect password. {card.FailedPasswordAttempts} failed attempt(s).");
                return;
            }

            if (card.FailedPasswordAttempts > 0)
            {
                card.FailedPasswordAttempts = 0;
                _repository.UpdateCard(card);
            }

            Console.WriteLine("Login successful!");
        }
        public void Transfer(string sourceCardNumber, string destinationCardNumber, float amount)
        {
            if (sourceCardNumber.Length != 16 || destinationCardNumber.Length != 16)
                throw new Exception("Card numbers must be 16 digits.");

            if (amount <= 0)
                throw new Exception("The amount must be greater than zero.");

            var sourceCard = _repository.GetCardByNumber(sourceCardNumber);
            var destCard = _repository.GetCardByNumber(destinationCardNumber);

            if (sourceCard == null || !sourceCard.IsActive)
                throw new Exception("The origin card is not valid or active.");

            if (destCard == null || !destCard.IsActive)
                throw new Exception("The destination card is not valid or active.");

            if (sourceCard.Balance < amount)
                throw new Exception("There is not enough balance.");

            sourceCard.Balance -= amount;
            destCard.Balance += amount;

            _repository.UpdateCard(sourceCard);
            _repository.UpdateCard(destCard);

            var transaction = new Transaction
            {
                SourceCardNumber = sourceCardNumber,
                DestinationCardNumber = destinationCardNumber,
                Amount = amount,
                TransactionDate = DateTime.Now,
                IsSuccesful = true
            };

            _repository.AddTransaction(transaction);
        }
        public List<Transaction> GetTransactions(string cardNumber)
        {
            return _repository.GetTransactions(cardNumber);
        }
    }
}
