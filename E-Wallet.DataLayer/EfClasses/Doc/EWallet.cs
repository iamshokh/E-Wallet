using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Wallet.DataLayer.EfClasses
{
    [Table("e_wallet")]
    public partial class EWallet
    {
        public EWallet()
        {
            EWalletTransactions = new HashSet<EWalletTransaction>();
        }
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("account_number")]
        [StringLength(20)]
        public string AccountNumber { get; set; } = null!;

        [Column("balance")]
        [Precision(18, 2)]
        public decimal Balance { get; set; }

        [Column("state_id")]
        public int StateId { get; set; }

        [Column("created_date", TypeName = "timestamp without time zone")]
        public DateTime CreatedDate { get; set; }

        [Column("created_user_id")]
        public int? CreatedUserId { get; set; }

        [Column("modified_date", TypeName = "timestamp without time zone")]
        public DateTime? ModifiedDate { get; set; }

        [Column("modified_user_id")]
        public int? ModifiedUserId { get; set; }

        [InverseProperty(nameof(EWalletTransaction.EWallet))]
        public virtual ICollection<EWalletTransaction> EWalletTransactions { get; } = new List<EWalletTransaction>();

        [ForeignKey(nameof(StateId))]
        public virtual State State { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } = null!;
    }
}
