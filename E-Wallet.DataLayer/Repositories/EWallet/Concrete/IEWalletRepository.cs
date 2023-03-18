using E_Wallet.DataLayer.EfClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Wallet.DataLayer.Repositories
{
    public interface IEWalletRepository
    {
        EWallet CreateWallet(EWalletDlDto dto);
    }
}
