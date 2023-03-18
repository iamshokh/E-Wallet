using E_Wallet.Core.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SwaggerServiceExtentions
    {
        public static void ConfigureSwaggerServices(this IServiceCollection services)
        {
            
            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(x => x.FullName);

                c.SwaggerDoc("v1",
                             new OpenApiInfo
                             {
                                 Title = "E_Wallet.WebApi",
                                 Version = "v1"
                             });

                c.SchemaFilter<SwaggerExcludeFilter>();
                //c.DescribeAllEnumsAsStrings();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. <br />
                                    Enter 'Bearer' [space] and then your token in the text input below. <br /><br />
                                    Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                    });

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

            });
            //services.AddSwaggerGenNewtonsoftSupport();
        }
    }

    public class SwaggerExcludeFilter : 
        ISchemaFilter
    {
        public void Apply(OpenApiSchema schema,
                          SchemaFilterContext context)
        {
            if (schema?.Properties == null ||
                context.Type == null)
                    return;

            foreach (PropertyInfo item in from t in context.Type.GetProperties()
                                          where t.GetCustomAttribute<SwaggerExcludeAttribute>() != null || t.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>() != null || t.GetCustomAttribute<JsonIgnoreAttribute>() != null
                                          select t)
                if (schema.Properties.ContainsKey(item.Name))
                    schema.Properties.Remove(item.Name);
        }
    }
}
