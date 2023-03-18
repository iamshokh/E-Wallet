using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace E_Wallet.DataLayer.PgSql;

public partial class EWalletContext : DbContext
{
    public EWalletContext()
    {
    }

    public EWalletContext(DbContextOptions<EWalletContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EWallet> EWallets { get; set; }

    public virtual DbSet<EWalletTransaction> EWalletTransactions { get; set; }

    public virtual DbSet<EnumDirectionType> EnumDirectionTypes { get; set; }

    public virtual DbSet<EnumState> EnumStates { get; set; }

    public virtual DbSet<SysUser> SysUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Username=postgres;Password=postgres;Database=E-Wallet");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EWallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("e_wallet_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("now()");

            entity.HasOne(d => d.State).WithMany(p => p.EWallets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_state_id");

            entity.HasOne(d => d.User).WithMany(p => p.EWallets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_id");
        });

        modelBuilder.Entity<EWalletTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("e_wallet_transaction_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("now()");

            entity.HasOne(d => d.DirectionType).WithMany(p => p.EWalletTransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_direction_type_id");

            entity.HasOne(d => d.EWallet).WithMany(p => p.EWalletTransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_e_wallet_id");

            entity.HasOne(d => d.State).WithMany(p => p.EWalletTransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_state_id");
        });

        modelBuilder.Entity<EnumDirectionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("enum_direction_type_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<EnumState>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("enum_state_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<SysUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sys_user_pkey");

            entity.HasIndex(e => e.PhoneNumber, "sys_user_unique_index_phone_number")
                .IsUnique()
                .HasFilter("(phone_number IS NOT NULL)");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("now()");

            entity.HasOne(d => d.State).WithMany(p => p.SysUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_state_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
