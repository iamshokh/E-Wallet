using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace E_Wallet.DataLayer.PgSql;

[Table("e_wallet_transaction")]
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

    [Column("direction_type_id")]
    public int DirectionTypeId { get; set; }

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

    [ForeignKey("DirectionTypeId")]
    [InverseProperty("EWalletTransactions")]
    public virtual EnumDirectionType DirectionType { get; set; } = null!;

    [ForeignKey("EWalletId")]
    [InverseProperty("EWalletTransactions")]
    public virtual EWallet EWallet { get; set; } = null!;

    [ForeignKey("StateId")]
    [InverseProperty("EWalletTransactions")]
    public virtual EnumState State { get; set; } = null!;
}
