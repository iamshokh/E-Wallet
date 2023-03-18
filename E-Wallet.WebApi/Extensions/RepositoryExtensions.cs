using E_Wallet.BizLogicLayer.UserAccountServices;
using E_Wallet.DataLayer;
using E_Wallet.DataLayer.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RepositoryExtensions
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
