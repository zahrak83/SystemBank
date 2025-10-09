using SystemBank.Dtos;

namespace SystemBank.Interface.IService
{
    public interface ITransactionService
    {
        List<GetTransactionDto> GetAll(string cardNumber);
        Result Transfer(string sourceCardNumber, string destinationCardNumber, float amount);
    }
}
