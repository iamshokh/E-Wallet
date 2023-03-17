using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Wallet.BizLogicLayer.EWalletServices
{
    public class EWalletTransactionDto
    {
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string State { get; set; }
    }
}
