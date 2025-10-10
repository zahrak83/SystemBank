using SystemBank.Dtos;
using SystemBank.Entities;

namespace SystemBank.Interface.IService
{
    public interface ICardService
    {
        public Result Login(string cardNumber, string password);
        Result ChangePassword(string cardNumber, string oldPassword, string newPassword);
        public string? GetHolderNameByCardNumber(string destinationCardNumber);
    }
}

