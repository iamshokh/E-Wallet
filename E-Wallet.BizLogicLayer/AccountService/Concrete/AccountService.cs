﻿using E_Wallet.BizLogicLayer.UserAccountServices;
using E_Wallet.Core;
using E_Wallet.Core.Security;
using E_Wallet.DataLayer.EfClasses;
using E_Wallet.DataLayer.EfCode;
using E_Wallet.DataLayer.Repositories;
using E_Wallet.DataLayer.Repositories.UserAccount;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StatusGeneric;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace E_Wallet.BizLogicLayer.AccountService
{
    public class AccountService : 
        StatusGenericHandler,
        IAccountService
    {
        private readonly JwtSettings settings;
        private readonly IUserAccountRepository repository;
        private readonly EfCoreContext context;

        public AccountService(
            JwtSettings settings,
            IUserAccountRepository repository,
            EfCoreContext context)
        {
            this.settings = settings;
            this.repository = repository;
            this.context = context;
        }

        private async Task<string> AuthenticateAsync(
            string username,
            string password
            )
        {
            if (!IsValidUser(username, password))
            {
                throw new AuthenticationException("Invalid username or password");
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var signingKey = 
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = settings.Issuer,
                Expires = DateTime.Now.AddMinutes(settings.ExpiresInMinutes),
                SigningCredentials = new SigningCredentials(signingKey,
                                                            SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin")
            })
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private bool IsValidUser(string userName,
                                 string password)
        {
            if (string.IsNullOrWhiteSpace(userName) ||
                string.IsNullOrWhiteSpace(password))
            {
                AddError("Имя пользователя или пароль неправильно");
                return false;
            }

            var user = repository.ByUserName(userName);

            if (user == null)
            {
                AddError("Имя пользователя или пароль неправильно");
                return false;
            }

            if (IsValid)
            {
                //AddUserLog(UserLogAction.LoginByPassword, user.UserName, user.Id);
                //_repository.UpdateUserLastAccessTime(user.Id);
                //string token = _authService.GenerateToken(user.UserName, user.UserRoles.IsActive().FirstOrDefault().RoleId);
                ////_authService.ResetUserName(user.UserName);
                ////_authService.ResetRoleId(user.UserRoles.IsActive().FirstOrDefault().RoleId);

                //return new UserLoginResultDto
                //{
                //    Token = token
                //};
            }

            return true;
        }

        public async Task<string> Login(LoginDto dto)
        {
            try
            {
                string result = await AuthenticateAsync(dto.UserName, dto.Password);
                if (!IsValid)
                {
                    AddError("Ошибка при Аутентификации");
                    return null;
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> Regester(RegisterateUserDlDto dto)
        {
            try
            {
                Random rd = new Random();
                string number = "";
                for (int i = 0; i < 12; i++)
                {
                    number += rd.Next(0, 9).ToString();
                }

                var user = repository.Registrate(dto);
                if(!IsValid)
                {
                    AddError("Ошибка при Регистрации");
                    return null;
                }

                var eWallet = new EWallet()
                {
                    AccountNumber = "8600" + number,
                    StateId = StateIdConst.ACTIVE,
                    Balance = 0m,
                    User = user
                };
                context.Add(eWallet);
                context.SaveChanges();

                string result = await AuthenticateAsync(dto.UserName, dto.Password);

                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}