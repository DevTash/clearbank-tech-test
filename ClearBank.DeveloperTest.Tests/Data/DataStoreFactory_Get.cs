using ClearBank.DeveloperTest.Config;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using FluentAssertions;
using Microsoft.Extensions.Options;

namespace ClearBank.DeveloperTest.Tests.Data
{
    public class DataStoreFactory_Get
    {
        [Fact]
        public void Should_Return_BackupAccountDataStore_When_DataStoreType_Is_Backup()
        {
            var appSettings = Options.Create(new AppSettings
            {
                DataStoreType = DataStoreType.Backup
            });
            var factory = new DataStoreFactory(appSettings);

            var dataStore = factory.Get();

            dataStore.Should().BeOfType<BackupAccountDataStore>();
        }

        [Fact]
        public void Should_Return_AccountDataStore_When_DataStoreType_Is_Not_Backup()
        {
            var appSettings = Options.Create(new AppSettings
            {
                DataStoreType = "SomeOtherType"
            });
            var factory = new DataStoreFactory(appSettings);

            var dataStore = factory.Get();

            dataStore.Should().BeOfType<AccountDataStore>();
        }
    }
}