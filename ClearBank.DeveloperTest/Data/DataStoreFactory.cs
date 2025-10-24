using ClearBank.DeveloperTest.Config;
using Microsoft.Extensions.Options;

namespace ClearBank.DeveloperTest.Data
{
    public class DataStoreFactory
    {
        private readonly AppSettings _appSettings;

        public DataStoreFactory(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public IDataStore Get()
        {
            return _appSettings.DataStoreType switch
            {
                DataStoreType.Backup => new BackupAccountDataStore(),
                _ => new AccountDataStore(),
            };
        }
    }
}