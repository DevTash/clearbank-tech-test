using ClearBank.DeveloperTest.Config;
using ClearBank.DeveloperTest.Types;
using Microsoft.Extensions.Options;

namespace ClearBank.DeveloperTest.Data
{
    public class AccountDataStoreFactory
    {
        private readonly AppSettings _appSettings;

        public AccountDataStoreFactory(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public IAccountDataStore Get()
        {
            return _appSettings.DataStoreType switch
            {
                DataStoreType.Backup => new BackupAccountDataStore(),
                _ => new AccountDataStore(),
            };
        }
    }
}