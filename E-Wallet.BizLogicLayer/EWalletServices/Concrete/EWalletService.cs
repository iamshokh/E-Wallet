using E_Wallet.Core;
using E_Wallet.DataLayer.EfClasses;
using E_Wallet.DataLayer.EfCode;
using E_Wallet.DataLayer.Repositories;
using E_Wallet.DataLayer.Repositories.EWalletTransaction;
using Microsoft.EntityFrameworkCore;
using StatusGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace E_Wallet.BizLogicLayer.EWalletServices
{
    public class EWalletService : StatusGenericHandler, IEwalletService
    {
        public readonly EfCoreContext _context;
        public EWalletService(EfCoreContext context)
        {
            _context = context;
        }

        public UserDto AccountExists(string accountNumber)
        {
            var eWallet = _context.EWallets.FirstOrDefault(a => a.AccountNumber == accountNumber);
            if (eWallet == null)
            {
                AddError("Не существует кошелька");
                return null;
            }
            var user = _context.Users.FirstOrDefault(a => a.Id == eWallet.UserId);
            if (eWallet == null)
            {
                AddError("Не существует аккаунта");
                return null;
            }

            var result = new UserDto()
            {
                Id = eWallet.UserId,
                Email = user.Email,
                Fullname = user.Fullname,
                PhoneNumber = user.PhoneNumber,
                Shortname = user.Shortname,
                UserName = user.UserName
            };
            return result;
        }

        public async Task<string> ReplenishWallet(ReplenishWalletRequestDto dto)
        {
            var eWallet = _context.EWallets.FirstOrDefault(a => a.UserId == dto.UserId);
            if (eWallet == null)
            {
                return new Exception("Не сущществует такого пользователя").Message;
            }
            var user = _context.Users.FirstOrDefault(a => a.Id == dto.UserId);
            if (!eWallet.User.IsIdentificate && eWallet.Balance + dto.Amount > 10000)
            {
                return new Exception("Максимальный баланс для неидентифицированного счета составляет 10000 сомони.").Message;
            }

            if (eWallet.User.IsIdentificate && eWallet.Balance + dto.Amount > 100000)
            {
                return new Exception("Максимальный остаток на идентифицированном счете составляет 100 000 сомони.").Message;
            }

            eWallet.Balance += dto.Amount;
            var result = new EWalletTransaction
            {
                Amount = dto.Amount,
                EWalletId = eWallet.Id,
                StateId = StateIdConst.ACTIVE,
                DirectionTypeId = DirectionTypeIdConst.REPLENISHMENT
            };

            _context.Add(result);
            _context.SaveChanges();

            return "Счёт заполнен";
        }

        public EWalletDto MonthlyOperations(string accountNumber)
        {
            DateTime startOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            var eWallet = _context.EWallets.FirstOrDefault(a => a.AccountNumber == accountNumber);
            if (eWallet == null)
            {
                AddError("Не сущществует такого пользователя");
                return null;
            }

            var user = _context.Users.FirstOrDefault(a => a.Id == eWallet.UserId);
            if (eWallet == null)
            {
                AddError("Не существует аккаунта");
                return null;
            }

            var eWalletTransactions = _context.EWalletTransactions.Where(a => a.EWalletId == eWallet.Id).ToList();
            var eWalletTransactionsDto = new List<EWalletTransactionDto>();
            foreach (var transaction in eWalletTransactions)
            {
                eWalletTransactionsDto.Add(new EWalletTransactionDto
                {
                    Amount = transaction.Amount,
                    State =_context.States.FirstOrDefault(a => a.Id == transaction.StateId).FullName,
                    DirectionType = _context.DirectionTypes.FirstOrDefault(a => a.Id == transaction.DirectionTypeId).FullName
                });
            }
            var result = new EWalletDto()
            {
                Count = eWalletTransactionsDto.Count.ToString() + " транзакции в этом месяце",
                AccountNumber = eWallet.AccountNumber,
                Balance = eWallet.Balance.ToString() + " сомони",
                User = user.UserName,
                EWalletTransactions = eWalletTransactionsDto
            };

            return result;
        }

        public string GetBalance(string accountNumber)
        {
            string money = _context.EWallets.FirstOrDefault(a => a.AccountNumber == accountNumber).Balance.ToString();
            return $"Ваш баланс на кошельке {money} сомони.";
        }
    }
}
