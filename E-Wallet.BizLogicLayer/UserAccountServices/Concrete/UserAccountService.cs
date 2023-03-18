using E_Wallet.Core;
using E_Wallet.DataLayer.EfClasses;
using E_Wallet.DataLayer.EfCode;
using E_Wallet.DataLayer.Repositories;
using E_Wallet.DataLayer.Repositories.UserAccount;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StatusGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace E_Wallet.BizLogicLayer.UserAccountServices
{
    public class UserAccountService : StatusGenericHandler, IUserAccountService
    {
        private readonly IUserAccountRepository _repository;
        private readonly HttpContext _httpContext;
        private readonly EfCoreContext _context;

        public UserAccountService(IUserAccountRepository repository, EfCoreContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<string> Registrate(RegisterateUserDlDto dto)
        {
            Random rd = new Random();
            string number = "";
            for (int i = 0; i < 12; i++)
            {
                number += rd.Next(0, 9).ToString();
            }

            var user = _repository.Registrate(dto);
            if (HasErrors)
                return null;

            var eWallet = new EWallet()
            {
                AccountNumber = "8600" + number,
                StateId = StateIdConst.ACTIVE,
                Balance = 0m,
                User = user
            };

            _context.Add(eWallet);
            _context.SaveChanges();

            return "Регистрация прошло успешно и вам автоматически открылся счёт";

        }

        public async Task<UserLoginResultDto> Login(UserLoginDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.UserName) || string.IsNullOrWhiteSpace(dto.Password))
            {
                AddError("Имя пользователя или пароль неправильно");
                return null;
            }

            var user = _repository.ByUserName(dto.UserName);

            if (user == null) // || !user.IsValidPassword(dto.Password))
            {
                AddError("Имя пользователя или пароль неправильно");

                //if (user != null)
                //    AddUserLog(UserLogAction.IncorrectPasswordEntered, user.UserName, user.Id);
                //return null;
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

            return null;
        }
    }
}
