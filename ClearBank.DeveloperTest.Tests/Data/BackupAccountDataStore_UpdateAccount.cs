using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using FluentAssertions;

namespace ClearBank.DeveloperTest.Tests.Data
{
    public class BackupAccountDataStore_UpdateAccount
    {
        private readonly BackupAccountDataStore _sut;

        public BackupAccountDataStore_UpdateAccount()
        {
            _sut = new BackupAccountDataStore();
        }

        [Fact]
        public void Should_Not_Throw()
        {
            Action act = () => _sut.UpdateAccount(new Account());

            act.Should().NotThrow();
        }
    }
}