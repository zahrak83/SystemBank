using SystemBank.Dtos;
using SystemBank.Interface.IRepository;

namespace SystemBank.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        public void Create(CreateTransactionDto model)
        {
            throw new NotImplementedException();
        }

        public List<GetTransactionDto> GetAll(string cardNumber)
        {
            throw new NotImplementedException();
        }
    }
}
