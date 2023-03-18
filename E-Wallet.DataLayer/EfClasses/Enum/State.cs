using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Wallet.DataLayer.EfClasses
{
    [Table("enum_state")]
    public partial class State
    {
        public State()
        {
            EWalletTransactions = new HashSet<EWalletTransaction>();
            EWallets = new HashSet<EWallet>();
            Users = new HashSet<User>();
        }
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("code")]
        [StringLength(50)]
        public string? Code { get; set; }

        [Column("short_name")]
        [StringLength(250)]
        public string ShortName { get; set; } = null!;

        [Column("full_name")]
        [StringLength(250)]
        public string FullName { get; set; } = null!;

        [Column("created_date", TypeName = "timestamp without time zone")]
        public DateTime CreatedDate { get; set; }

        [InverseProperty(nameof(EWalletTransaction.State))]
        public virtual ICollection<EWalletTransaction> EWalletTransactions { get; set; }

        [InverseProperty(nameof(EWallet.State))]
        public virtual ICollection<EWallet> EWallets { get; } = new List<EWallet>();

        [InverseProperty(nameof(User.State))]
        public virtual ICollection<User> Users { get; } = new List<User>();
    }
}
