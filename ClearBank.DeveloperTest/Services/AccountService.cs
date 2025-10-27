using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountDataStoreFactory _accountDataStoreFactory;

        public AccountService(IAccountDataStoreFactory accountDataStoreFactory)
        {
            _accountDataStoreFactory = accountDataStoreFactory;
        }

        public void DebitAccount(Account account, decimal amount)
        {
            account.Balance -= amount;
            _accountDataStoreFactory.Get().UpdateAccount(account);
        }

        public Account GetAccount(string accountNumber)
        {
            return _accountDataStoreFactory.Get().GetAccount(accountNumber);
        }
    }
}
