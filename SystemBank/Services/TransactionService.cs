using SystemBank.Dtos;
using SystemBank.Infrastructure.Repositories;
using SystemBank.Interface.IRepository;
using SystemBank.Interface.IService;

namespace SystemBank.Services
{
    public class TransactionService : ITransactionService
    {
        public readonly ITransactionRepository _transactionRepository;
        private readonly ICardRepository _cardRepository;

        public TransactionService()
        {
            _transactionRepository = new TransactionRepository();
            _cardRepository = new CardRepository();
        }

        public List<GetTransactionDto> GetAll(string cardNumber)
        {
            return _transactionRepository.GetAll(cardNumber);
        }
        public Result Transfer(string sourceCardNumber, string destinationCardNumber, float amount)
        {
            bool transactionStatus = false;

            if (amount <= 0)
                return new Result { IsSuccess = false, Message = "The transfer amount must be greater than 0" };

            if (sourceCardNumber.Length != 16)
                return new Result { IsSuccess = false, Message = "sourceCardNumber is not valid" };

            if (destinationCardNumber.Length != 16)
                return new Result { IsSuccess = false, Message = "destinationCardNumber is not valid" };

            if (sourceCardNumber == destinationCardNumber)
                return new Result { IsSuccess = false, Message = "Cannot transfer to the same card" };

            if (!_cardRepository.CardIsActive(sourceCardNumber))
                return new Result { IsSuccess = false, Message = "sourceCardNumber is not Active" };

            if (!_cardRepository.CardIsActive(destinationCardNumber))
                return new Result { IsSuccess = false, Message = "destinationCardNumber is not Active" };

            var destinationCard = _cardRepository.GetCardByNumber(destinationCardNumber);
            if (destinationCard == null)
                return new Result { IsSuccess = false, Message = "Destination card does not exist" };

            var sourceBalance = _cardRepository.GetBalance(sourceCardNumber);

            var fee = amount > 1000f ? amount * 0.015f : amount * 0.005f;
            var totalDeduction = amount + fee;

            if (sourceBalance < totalDeduction)
                return new Result { IsSuccess = false, Message = "Your card doesn't have enough balance for this transaction" };


            var dailyWithdrawal = _transactionRepository.DailyWithdrawal(sourceCardNumber);

            if ((dailyWithdrawal + amount) > 250)
            {
                return new Result 
                {
                    IsSuccess = false,
                    Message = "youre daily transfer limit is full"
                };
            }

            var destinationBalance = _cardRepository.GetBalance(destinationCardNumber);

            try
            {
                _cardRepository.SetBalance(sourceCardNumber, sourceBalance - amount);
                _cardRepository.SetBalance(destinationCardNumber, destinationBalance + amount);
                _cardRepository.SaveChanges();
                transactionStatus = true;
            }
            catch
            {
                transactionStatus = false;
            }
            finally
            {
                _transactionRepository.Create(new CreateTransactionDto
                {
                    Amount = amount,
                    Fee = fee,
                    DestinationCardNumber = destinationCardNumber,
                    SourceCardNumber = sourceCardNumber,
                    IsSuccessful = transactionStatus
                });
            }
            if (transactionStatus)
            {
                return new Result 
                {
                    IsSuccess = true,
                    Message = "Transfer money succeeded" 
                };
            }
                
            else
            {
                return new Result 
                { 
                    IsSuccess = false,
                    Message = "Transfer failed due to an error" 
                };
            }
                
        }
    }
}