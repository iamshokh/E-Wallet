using E_Wallet.DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Wallet.DataLayer.EfCode
{
    public class EfCoreContext : DbContext
    {
        public EfCoreContext(DbContextOptions options)
            : base(options)
        {
           
        }

        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<E_Wallet.DataLayer.EfClasses.Type> Types { get; set; }
        public virtual DbSet<EWallet> EWallets { get; set; }
        public virtual DbSet<EWalletTransaction> EWalletTransactions { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
