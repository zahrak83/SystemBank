using SystemBank.Entities;
using SystemBank.Infrastructure;
using SystemBank.Interface.IService;

namespace SystemBank.Services
{
    public class CardService : ICardService
    {
        private readonly Car _repository = new Car();

        public Card Authenticate(string cardNumber, string password)
        {
            if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length != 16)
            {
                throw new ArgumentException("Card number must be exactly 16 digits.");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password cannot be empty.");
            }

            var card = _repository.GetCardByNumber(cardNumber);
            if (card == null)
            {
                throw new Exception("Card not found.");
            }

            if (!card.IsActive)
            {
                throw new Exception("This card is blocked or not active.");
            }

            if (card.Password != password)
            {
                card.FailedPasswordAttempts++;

                if (card.FailedPasswordAttempts >= 3)
                {
                    card.IsActive = false;
                    _repository.UpdateCard(card);
                    throw new Exception("Your card was blocked due to 3 unsuccessful attempts.");
                }

                _repository.UpdateCard(card);
                throw new Exception($"Incorrect password. {card.FailedPasswordAttempts} failed attempt.");
            }

            if (card.FailedPasswordAttempts > 0)
            {
                card.FailedPasswordAttempts = 0;
                _repository.UpdateCard(card);
            }

            return card;
        }
        public Card Transfer(string sourceCardNumber, string destinationCardNumber, float amount)
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

            return sourceCard;
        }
        public List<Transaction> GetTransactions(string cardNumber)
        {
            return _repository.GetTransactions(cardNumber);
        }

    }
}
