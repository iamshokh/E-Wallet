using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Wallet.DataLayer.Repositories.UserAccount;
using StatusGeneric;

namespace E_Wallet.BizLogicLayer.UserAccountServices
{
    public interface IUserAccountService : IStatusGeneric
    {
        Task<UserLoginResultDto> Login(UserLoginDto dto);
        Task<string> Registrate(RegisterateUserDlDto dto);
    }
}
