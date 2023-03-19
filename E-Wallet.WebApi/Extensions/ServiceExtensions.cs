using E_Wallet.BizLogicLayer.AccountService;
using E_Wallet.BizLogicLayer.EWalletServices;
using E_Wallet.BizLogicLayer.UserAccountServices;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEwalletService, EWalletService>();
        }
    }
}
