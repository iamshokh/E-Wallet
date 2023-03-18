using E_Wallet.BizLogicLayer.AccountService;
using E_Wallet.WebApi;
using E_Wallet.WebApi.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

AppSettings.Init(builder.Configuration.Get<AppSettings>());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureDbServices();
builder.Services.ConfigureRepositories();
builder.Services.ConfigureSwaggerServices();
builder.Services.ConfigureServices();
builder.Services.ConfigureConfigs();
builder.Services.AddSingleton(AppSettings.Instance.Jwt);
builder.Services.AddScoped<IAccountService, AccountService>();

var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.Instance.Jwt.SecretKey));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = AppSettings.Instance.Jwt.Issuer,
            IssuerSigningKey = signingKey
        };

    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.ConfigureSwagger();

app.MapControllers();

app.Run();
