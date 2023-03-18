using E_Wallet.DataLayer.EfCode;
using E_Wallet.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Wallet.DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {


        private readonly EfCoreContext _context;
        public UnitOfWork(EfCoreContext context)
        {
            _context = context;
            UserAccounts = new UserAccountRepository(_context);
        }

        public IUserAccountRepository UserAccounts { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
