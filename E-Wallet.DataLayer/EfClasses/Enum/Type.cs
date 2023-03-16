using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Wallet.DataLayer.EfClasses
{
    [Table("enum_type")]
    public partial class Type
    {
        public Type()
        {
            EWalletTransactions = new HashSet<EWalletTransaction>();
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

        [InverseProperty(nameof(EWalletTransaction.Type))]
        public virtual ICollection<EWalletTransaction> EWalletTransactions { get; } = new List<EWalletTransaction>();
    }
}
