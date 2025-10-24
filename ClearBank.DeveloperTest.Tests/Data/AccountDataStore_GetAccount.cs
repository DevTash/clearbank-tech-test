using ClearBank.DeveloperTest.Data;
using FluentAssertions;

namespace ClearBank.DeveloperTest.Tests.Data
{

    public class AccountDataStore_GetAccount
    {
        private readonly AccountDataStore _sut;

        public AccountDataStore_GetAccount()
        {
            _sut = new AccountDataStore();
        }

        [Fact]
        public void Should_Return_Account_When_Account_Number_Is_Valid()
        {
            var account = _sut.GetAccount("ValidAccountNumber");
            
            account.Should().NotBeNull();
        }
    }
}