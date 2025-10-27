using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IAccountService
    {
        Account GetAccount(string accountNumber);
        void DebitAccount(Account account, decimal amount);
    }
}