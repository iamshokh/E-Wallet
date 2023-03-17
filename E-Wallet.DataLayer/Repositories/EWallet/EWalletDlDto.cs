using E_Wallet.DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Wallet.DataLayer.Repositories.EWalletTransaction;

namespace E_Wallet.DataLayer.Repositories
{
    public class EWalletDlDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string AccountNumber { get; set; } = null!;

        public decimal Balance { get; set; }

        public int StateId { get; set; }

        public List<EWalletTransactionDlDto> EWalletTransactions { get; set; } = new();// List<EWalletTransactionDlDto>(); 
    }
}
