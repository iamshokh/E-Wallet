using E_Wallet.DataLayer.Repositories.EWalletTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Wallet.BizLogicLayer.EWalletServices
{
    public class ReplenishWalletRequestDto
    {
        public EWalletTransactionDlDto RequestDto { get; set; }
        public int UserId { get; set; }
        public string Digest { get; set; }
    }
}
