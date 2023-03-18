using E_Wallet.Core;
using E_Wallet.DataLayer.EfClasses;
using E_Wallet.DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Wallet.DataLayer.Repositories
{
    public class EWalletRepository : CommandContext<User>, IEWalletRepository
    {
        private readonly EfCoreContext _context;
        public EWalletRepository(EfCoreContext context)
        {
            _context = context;
        }

        public EWallet CreateWallet(EWalletDlDto dto)
        {
            Random rd = new Random();
            string number = "";
            for (int i = 0; i < 12; i++)
            {
                number += rd.Next(0, 9).ToString();
            }
            var entity = new EWallet();
            entity.AccountNumber = "8600" + number;
            entity.UserId = dto.UserId;
            entity.StateId = StateIdconst.ACTIVE;
            entity.Balance = 0m;
            return entity;
        }
    }
}
