using E_Wallet.WebApi;

namespace Microsoft.AspNetCore.Builder
{
    public static class SwaggerAppBuilderExtentions
    {
        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            if (AppSettings.Instance.Swagger.Enabled)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "E_Wallet.WebApi v1"));
            }
        }
    }
}
