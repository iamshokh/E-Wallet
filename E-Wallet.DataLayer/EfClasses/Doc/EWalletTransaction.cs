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
    public partial class EWalletTransaction
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("e_wallet_id")]
        public int EWalletId { get; set; }

        [Column("amount")]
        [Precision(18, 2)]
        public decimal Amount { get; set; }

        [Column("type_id")]
        public int TypeId { get; set; }

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

        [ForeignKey(nameof(EWalletId))]
        public virtual EWallet EWallet { get; set; } = null!;

        [ForeignKey(nameof(StateId))]
        public virtual State State { get; set; } = null!;

        [ForeignKey(nameof(TypeId))]
        public virtual Type Type { get; set; } = null!;
    }
}
