using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace E_Wallet.DataLayer.PgSql;

[Table("e_wallet")]
public partial class EWallet
{
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

    [InverseProperty("EWallet")]
    public virtual ICollection<EWalletTransaction> EWalletTransactions { get; } = new List<EWalletTransaction>();

    [ForeignKey("StateId")]
    [InverseProperty("EWallets")]
    public virtual EnumState State { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("EWallets")]
    public virtual SysUser User { get; set; } = null!;
}
