using E_Wallet.BizLogicLayer.UserAccountServices;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserAccountService, UserAccountService>();
        }
    }
}
