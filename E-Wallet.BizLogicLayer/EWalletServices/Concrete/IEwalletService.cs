using E_Wallet.DataLayer.Repositories.EWalletTransaction;
using StatusGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Wallet.BizLogicLayer.EWalletServices
{
    public interface IEwalletService : IStatusGeneric
    {
        UserDto AccountExists(int userId, string digest);
        Task<string> ReplenishWallet(EWalletTransactionDlDto dto, int userId, string digest);
        EWalletDto MonthlyOperations(int userId, string digest);
        decimal GetBalance(int userId, string digest);
    }
}
