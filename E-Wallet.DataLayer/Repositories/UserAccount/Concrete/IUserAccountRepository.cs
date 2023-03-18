using E_Wallet.DataLayer.EfClasses;
using E_Wallet.DataLayer.EfCode;
using E_Wallet.DataLayer.Repositories.UserAccount;
using Microsoft.EntityFrameworkCore;
using StatusGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Wallet.DataLayer.Repositories
{
    public interface IUserAccountRepository : ICommandContext<User>
    {
        User ByUserName(string userName);
        User Registrate(RegisterateUserDlDto dto);
    }
}
