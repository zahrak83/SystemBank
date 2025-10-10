using SystemBank.Dtos;
using SystemBank.Entities;
using SystemBank.Infrastructure;
using SystemBank.Infrastructure.Repositories;
using SystemBank.Interface.IRepository;
using SystemBank.Interface.IService;

namespace SystemBank.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService()
        {
            _cardRepository = new CardRepository();
        }

        public Result ChangePassword(string cardNumber, string oldPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                return new Result { IsSuccess = false, Message = "New password cannot be empty." };

            var isValid = _cardRepository.CardExist(cardNumber, oldPassword);
            if (!isValid)
                return new Result { IsSuccess = false, Message = "Current password is incorrect." };

            _cardRepository.SetPassword(cardNumber, newPassword);
            _cardRepository.SaveChanges();

            return new Result { IsSuccess = true, Message = "Password changed successfully." };
        }

        public Result Login(string cardNumber, string password)
        {
            if (string.IsNullOrWhiteSpace(cardNumber) || string.IsNullOrWhiteSpace(password))
            {
                return new Result
                {
                    IsSuccess = false,
                    Message = "Card number and password cannot be empty."
                };
            }

            if (cardNumber.Length != 16)
            {
                return new Result
                {
                    IsSuccess = false,
                    Message = "Invalid card number."
                };
            }

            var card = _cardRepository.GetCardByNumber(cardNumber);
            if (card == null)
            {
                return new Result
                {
                    IsSuccess = false,
                    Message = "Card not found."
                };
            }

            if (!_cardRepository.CardIsActive(cardNumber))
            {
                return new Result
                {
                    IsSuccess = false,
                    Message = "This card has been blocked."
                };
            }

            var tryCount = _cardRepository.GetWrongPasswordTry(cardNumber);

            var cardExist = _cardRepository.CardExist(cardNumber, password);

            if (cardExist)
            {
                if (tryCount > 0)
                {
                    _cardRepository.ClearWrongPasswordTry(cardNumber);
                    _cardRepository.SaveChanges();
                }

                return new Result { IsSuccess = true, Message = "Login successful." };
            }
            else
            {
                _cardRepository.SetWrongPasswordTry(cardNumber);
                _cardRepository.SaveChanges();

                var newTryCount = _cardRepository.GetWrongPasswordTry(cardNumber);
                if (newTryCount >= 3)
                {
                    _cardRepository.SetDeactive(cardNumber);
                    _cardRepository.SaveChanges();

                    return new Result
                    {
                        IsSuccess = false,
                        Message = "3 unsuccessful attempts! Your card has been blocked."
                    };
                }
                return new Result
                {
                    IsSuccess = false,
                    Message = "Card number or password is wrong."
                };
            }
        }
        public string? GetHolderNameByCardNumber(string destinationCardNumber)
        {
            var card = _cardRepository.GetCardByNumber(destinationCardNumber);
            return card?.HolderName;
        }
    }
}

        
