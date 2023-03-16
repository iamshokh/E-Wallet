using E_Wallet.Core.Configurations;

namespace E_Wallet.WebApi
{
    public class AppSettings
    {
        public static AppSettings Instance { get; private set; }
        public DatabaseConfig Database { get; set; }
    }
}
