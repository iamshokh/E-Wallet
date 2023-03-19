namespace E_Wallet.WebApi.Extensions
{
    public static class ConfigServiceExtentions
    {
        public static void ConfigureConfigs(this IServiceCollection services)
        {
            services.AddSingleton(AppSettings.Instance.Database);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
