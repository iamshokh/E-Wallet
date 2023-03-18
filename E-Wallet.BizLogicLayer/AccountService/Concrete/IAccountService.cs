using E_Wallet.BizLogicLayer.UserAccountServices;
using E_Wallet.DataLayer.Repositories.UserAccount;
using StatusGeneric;

namespace E_Wallet.BizLogicLayer.AccountService
{
    public interface IAccountService : 
        IStatusGeneric
    {
        Task<string> Login(LoginDto dto);
        Task<string> Regester(RegisterateUserDlDto dto);

    }
}
