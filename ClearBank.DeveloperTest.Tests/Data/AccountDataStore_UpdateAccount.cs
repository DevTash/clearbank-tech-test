using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using FluentAssertions;

namespace ClearBank.DeveloperTest.Tests.Data
{
    public class AccountDataStore_UpdateAccount
    {
        private readonly AccountDataStore _sut;

        public AccountDataStore_UpdateAccount()
        {
            _sut = new AccountDataStore();
        }

        [Fact]
        public void Should_Not_Throw()
        {
            Action act = () => _sut.UpdateAccount(new Account());
            
            act.Should().NotThrow();
        }
    }
}