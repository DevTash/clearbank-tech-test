using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using NSubstitute;

namespace ClearBank.DeveloperTest.Tests.Services
{
    public class AccountService_DebitAccount
    {
        private readonly IAccountDataStore _accountDataStore;
        private readonly IAccountDataStoreFactory _accountDataStoreFactory;
        private readonly AccountService _sut;

        public AccountService_DebitAccount()
        {
            _accountDataStore = Substitute.For<IAccountDataStore>();
            _accountDataStoreFactory = Substitute.For<IAccountDataStoreFactory>();

            _accountDataStoreFactory.Get().Returns(_accountDataStore);

            _sut = new AccountService(_accountDataStoreFactory);
        }

        [Fact]
        public void Should_Update_Account_With_Correct_Amount()
        {
            var account = new Account { Balance = 100m };

            _sut.DebitAccount(account, 40m);

            _accountDataStore.Received(1).UpdateAccount(Arg.Is<Account>(x => x.Balance == 60m));
        }
    }
}
