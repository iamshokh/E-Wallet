using E_Wallet.DataLayer.EfCode;
using E_Wallet.DataLayer.PgSql.EfCode;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace E_Wallet.WebApi.Extensions
{
    public static class DbServiceExtentions
    {
        public static void ConfigureDbServices(this IServiceCollection services)
        {
            services.AddDbContext<EfCoreContext, PgSqlContext>(options =>
                options.UseNpgsql(AppSettings.Instance.Database.PgSql.ConnectionString));
                       //.AddInterceptors(new HintInterceptor()));
            services.AddScoped<DbContext>(x => x.GetService<EfCoreContext>());
        }
    }
}
