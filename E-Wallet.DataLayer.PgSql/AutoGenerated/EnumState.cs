using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace E_Wallet.DataLayer.PgSql;

[Table("enum_state")]
public partial class EnumState
{
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

    [InverseProperty("State")]
    public virtual ICollection<EWalletTransaction> EWalletTransactions { get; } = new List<EWalletTransaction>();

    [InverseProperty("State")]
    public virtual ICollection<EWallet> EWallets { get; } = new List<EWallet>();

    [InverseProperty("State")]
    public virtual ICollection<SysUser> SysUsers { get; } = new List<SysUser>();
}
