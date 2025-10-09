using SystemBank.Dtos;
using SystemBank.Entities;

namespace SystemBank.Interface.IService
{
    public interface ICardService
    {
        public Result Login(string cardNumber, string password);
    }
}

