using SystemBank.Dtos;

namespace SystemBank.Interface.IRepository
{
    public interface ITransactionRepository
    {
        void Create(CreateTransactionDto model);
        List<GetTransactionDto> GetAll(string cardNumber);
    }
}
