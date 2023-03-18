using E_Wallet.Core.Configurations;
using E_Wallet.Core.Security;

namespace E_Wallet.WebApi
{
    public class AppSettings
    {
        public static AppSettings Instance { get; private set; } = null!;
        public DatabaseConfig Database { get; set; } = null!;
        public JwtSettings Jwt { get; set; } = null!;
        public SwaggerConfig Swagger { get; set; } = new SwaggerConfig();

        public static void Init(AppSettings instance)
            => Instance = instance;        
    }

    public class SwaggerConfig
    {
        public bool Enabled { get; set; }
    }

}
