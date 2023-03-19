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
        UserDto AccountExists(string accountNumber);
        Task<string> ReplenishWallet(ReplenishWalletRequestDto dto);
        EWalletDto MonthlyOperations(string accountNumber);
        decimal GetBalance(string accountNumber);
    }
}
