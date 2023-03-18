using E_Wallet.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Wallet.DataLayer
{
    public interface IUnitOfWork : IDisposable
    {
        IUserAccountRepository UserAccounts { get; }
    }
}
