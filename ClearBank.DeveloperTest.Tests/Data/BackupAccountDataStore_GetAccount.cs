using ClearBank.DeveloperTest.Data;
using FluentAssertions;

namespace ClearBank.DeveloperTest.Tests.Data
{

    public class BackupAccountDataStore_GetAccount
    {
        private readonly BackupAccountDataStore _sut;

        public BackupAccountDataStore_GetAccount()
        {
            _sut = new BackupAccountDataStore();
        }

        [Fact]
        public void Should_Return_Account_When_Account_Number_Is_Valid()
        {
            var account = _sut.GetAccount("ValidAccountNumber");

            account.Should().NotBeNull();
        }
    }
}