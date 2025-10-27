using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using NSubstitute;

namespace ClearBank.DeveloperTest.Tests.Services
{
    public class AccountService_GetAccount
    {
        private readonly IAccountDataStore _accountDataStore;
        private readonly IAccountDataStoreFactory _accountDataStoreFactory;
        private readonly AccountService _sut;

        public AccountService_GetAccount()
        {
            _accountDataStore = Substitute.For<IAccountDataStore>();
            _accountDataStoreFactory = Substitute.For<IAccountDataStoreFactory>();

            _accountDataStoreFactory.Get().Returns(_accountDataStore);

            _sut = new AccountService(_accountDataStoreFactory);
        }

        [Fact]
        public void Should_Retrieve_Account()
        {
            var accountNumber = "ValidAccountNumber";

            _sut.GetAccount(accountNumber);

            _accountDataStore.Received(1).GetAccount(Arg.Is<string>(x => x == accountNumber));
        }
    }
}
