using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Wallet.DataLayer.Repositories.EWalletTransaction
{
    public class EWalletTransactionDlDto
    {
        [Required]
        public decimal Amount { get; set; }
    }
}
